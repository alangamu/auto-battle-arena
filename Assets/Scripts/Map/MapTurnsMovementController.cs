using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapTurnsMovementController : MonoBehaviour
    {
        [SerializeField]
        private GameObjectVariable _activeMapHero;
        [SerializeField]
        private GameObjectRuntimeSet _heroes;
        [SerializeField]
        private GameEvent _nextTurnEvent;
        [SerializeField]
        private TileRuntimeSet _tiles;
        //[SerializeField]
        //private HeroStatSO _sightStat;
        [SerializeField]
        private HeroStatSO _movementStat;

        private int _heroIndex;

        private void OnEnable()
        {
            //TODO: remove from here when changing scenes (battle)
            _heroIndex = 0;
            _nextTurnEvent.OnRaise += NextTurn;
        }

        private void OnDisable()
        {
            _nextTurnEvent.OnRaise -= NextTurn;
        }

        private void NextTurn()
        {
            ClearSelectedList();

            if (_activeMapHero.Value == null)
            {
                int heroIndex = _heroIndex++ % _heroes.Items.Count;
                print($"heroIndex {heroIndex}");
                GameObject activeHero = _heroes.Items[heroIndex];
                print($"activeHero {activeHero}");
                _activeMapHero.SetActiveGameObject(activeHero);
            }

            if (_activeMapHero.Value.TryGetComponent(out IMapUnitController mapUnitController))
            {
                Hex activeHeroHex = _tiles.GetHexAt(mapUnitController.GetHexCoordinatesQ(), mapUnitController.GetHexCoordinatesR());
                int movementPoints = 0; 

                if (_activeMapHero.Value.TryGetComponent(out IHeroController heroController))
                {
                    movementPoints = heroController.ThisHero.ThisCombatStats.StatCount(_movementStat.StatId);
                }

                List<ITile> neighborTiles = _tiles.GetNeighboursTiles(activeHeroHex, movementPoints);

                foreach (var neighborTile in neighborTiles)
                {
                    if (neighborTile.GetHex().IsWalkable)
                    {
                        if (_tiles.FindPath(activeHeroHex, neighborTile.GetHex()).Count <= movementPoints)
                        {
                            GameObject tileGameObject = neighborTile.GetGameObject();
                            if (tileGameObject.TryGetComponent(out ISelectable selectable))
                            {
                                selectable.Select(true);
                            }
                        }
                    }
                }
            }
        }

        private void ClearSelectedList()
        {
            //_tiles.Items.FindAll(x => x.GetGameObject().TryGetComponent())
        }
    }
}