using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Map;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapFogOfWarController : MonoBehaviour
    {
        [SerializeField]
        private TileRuntimeSet _tiles;
        [SerializeField]
        private GameEvent _showFogOfWarEvent;
        [SerializeField]
        private GameObjectVariable _activeHeroStandingTile;
        [SerializeField]
        private GameObjectVariable _activeMapHero;
        [SerializeField]
        private HeroStatSO _sightStat;
        [SerializeField]
        private TileStateSO _activeState; 
        [SerializeField]
        private TileStateSO _inactiveState;

        //TODO: remove this later
        [SerializeField]
        private bool _showInitialFogOfWar;

        private Dictionary<IHeroController, List<ITile>> _activeTiles;

        private void OnEnable()
        {
            _showFogOfWarEvent.OnRaise += InitialFogOfWar;
            _activeHeroStandingTile.OnValueChanged += ShowThisFogOfWar;
            _activeTiles = new Dictionary<IHeroController, List<ITile>>();
        }

        private void OnDisable()
        {
            _showFogOfWarEvent.OnRaise -= InitialFogOfWar;
            _activeHeroStandingTile.OnValueChanged -= ShowThisFogOfWar;
        }

        private void ShowThisFogOfWar(GameObject activeTile)
        {
            if (_activeMapHero.Value.TryGetComponent(out IHeroController heroController))
            {
                if (activeTile.TryGetComponent(out ITile tile))
                {
                    int sightPoints = heroController.ThisHero.ThisCombatStats.StatCount(_sightStat.StatId);
                    List<ITile> sightTiles = _tiles.GetNeighboursTiles(tile.GetHex(), sightPoints);

                    foreach (var item in sightTiles)
                    {
                        if (item.GetGameObject().TryGetComponent(out ITileVisionController visionController))
                        {
                            item.SetState(_activeState);
                            visionController.SetActive();
                        }
                    }
                    if (!_activeTiles.ContainsKey(heroController))
                    {
                        _activeTiles.Add(heroController, sightTiles);
                    }
                    else
                    {
                        _activeTiles[heroController] = sightTiles;
                    }
                }
            }

            UpdateVision();
        }

        private void UpdateVision()
        {
            List<ITile> activeTiles = _tiles.Items.FindAll(x => x.TileState == _activeState);
            List<ITile> newActiveTiles = new List<ITile>();

            foreach (var tile in _activeTiles)
            {
                foreach (var item in tile.Value)
                {
                    if (!newActiveTiles.Contains(item))
                    {
                        newActiveTiles.Add(item);
                    }
                }
            }

            foreach (var tile in activeTiles)
            {
                if (!newActiveTiles.Contains(tile))
                {
                    if (tile.GetGameObject().TryGetComponent(out ITileVisionController visionController))
                    {
                        tile.SetState(_inactiveState);
                        visionController.SetInactive();
                    }
                }
            }
        }

        private void InitialFogOfWar()
        {
            if (_showInitialFogOfWar)
            {
                foreach (var tile in _tiles.Items)
                {
                    GameObject tileGO = tile.GetGameObject();
                    if (tileGO.TryGetComponent(out ITileVisionController visionController))
                    {
                        visionController.SetHidden();
                    }
                }
            }
        }
    }
}