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
        private GameObjectVariable _activeHeroMoveToTile;
        [SerializeField]
        private HeroStatSO _sightStat;
        [SerializeField]
        private TileStateSO _activeState; 
        [SerializeField]
        private TileStateSO _inactiveState;
        [SerializeField]
        private GameObjectRuntimeSet _heroes;
        [SerializeField]
        private GameEvent _heroesReadyEvent;

        //TODO: remove this later
        [SerializeField]
        private bool _showInitialFogOfWar;

        private Dictionary<IHeroController, List<ITile>> _activeTiles;

        private void OnEnable()
        {
            _activeHeroMoveToTile.OnValueChanged += ShowThisFogOfWar;
            _activeTiles = new Dictionary<IHeroController, List<ITile>>();
            _showFogOfWarEvent.OnRaise += InitialFogOfWar;
            _heroesReadyEvent.OnRaise += ShowFogOfWar;
        }

        private void OnDisable()
        {
            _activeHeroMoveToTile.OnValueChanged -= ShowThisFogOfWar;
            _showFogOfWarEvent.OnRaise -= InitialFogOfWar;
            _heroesReadyEvent.OnRaise -= ShowFogOfWar;
        }

        private void ShowFogOfWar()
        {
            foreach (var hero in _heroes.Items)
            {
                ShowThisFogOfWar(hero);
            }
        }

        private void ShowThisFogOfWar(GameObject heroObject)
        {
            if (!_showInitialFogOfWar)
            {
                return;
            }
            if (heroObject.TryGetComponent(out IHeroController heroController))
            {
                if (heroObject.TryGetComponent(out IMapUnitController mapUnitController))
                {
                    int sightPoints = heroController.ThisHero.ThisCombatStats.StatCount(_sightStat.StatId);
                    List<ITile> sightTiles = _tiles.GetNeighboursTiles(mapUnitController.Q, mapUnitController.R, sightPoints);

                    foreach (var item in sightTiles)
                    {
                        if (item.GetGameObject().TryGetComponent(out ITileVisionController visionController))
                        {
                            item.SetState(_activeState);
                            _tiles.Map.Find(x => x.q == item.GetHex().Q && x.r == item.GetHex().R).tileState = _activeState;
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
                        _tiles.Map.Find(x => x.q == tile.GetHex().Q && x.r == tile.GetHex().R).tileState = _inactiveState;
                        visionController.SetInactive();
                    }
                }
            }
        }

        /// <summary>
        /// For every tile in the map sets hidden if _showInitialFogOfWar is true, inactive otherwise
        /// </summary>
        private void InitialFogOfWar()
        {
            if (_showInitialFogOfWar)
            {
                foreach (var tile in _tiles.Items)
                {
                    GameObject tileGO = tile.GetGameObject();

                    if (tileGO.TryGetComponent(out ITileVisionController visionController))
                    {
                        Hex hex = tile.GetHex();
                        HexBase hexBase = _tiles.Map.Find(x => x.q == hex.Q && x.r == hex.R);

                        if (hexBase.tileState == _inactiveState)
                            visionController.SetInactive();
                        else
                            visionController.SetHidden();
                    }
                }
            }
        }
    }
}