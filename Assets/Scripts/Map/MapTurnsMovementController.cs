using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapTurnsMovementController : MonoBehaviour
    {
        [SerializeField]
        private GameObjectVariable _activeMapHero;
        [SerializeField]
        private GameObjectGameEvent _selectTileEvent;
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
        [SerializeField]
        private WeaponSO _unarmed;

        private int _heroIndex;
        private List<ITile> _canWalkTiles;

        private void OnEnable()
        {
            //TODO: remove from here when changing scenes (battle)
            _heroIndex = 0;
            _nextTurnEvent.OnRaise += NextTurn;
            _selectTileEvent.OnRaise += SelectDestinationTileEvent;
        }

        private void SelectDestinationTileEvent(GameObject obj)
        {
            if (obj.TryGetComponent(out ITile tile))
            {
                if (!_canWalkTiles.Contains(tile))
                {
                    return;
                }
                _ = MakeMovementAsync(tile.GetHex());
            } 
        }

        private void OnDisable()
        {
            _nextTurnEvent.OnRaise -= NextTurn;
            _selectTileEvent.OnRaise -= SelectDestinationTileEvent;
        }

        public async Task MakeMovementAsync(Hex destinationHex)
        {
            GameObject activeHero = _activeMapHero.Value;
            if (activeHero.TryGetComponent(out IMapMovementController mapMovementController))
            {
                if (activeHero.TryGetComponent(out IMapUnitController mapUnitController))
                {
                    Hex startHex = _tiles.GetHexAt(mapUnitController.GetHexCoordinatesQ(), mapUnitController.GetHexCoordinatesR());
                    List<Vector3> path = new List<Vector3>();
                    List<Hex> hexes = _tiles.FindPath(startHex, destinationHex);

                    for (int i = hexes.Count - 1; i >= 0; i--)
                    {
                        path.Add(_tiles.GetTileAt(hexes[i].Q, hexes[i].R).GetGameObject().transform.position);
                    }

                    WeaponSO weapon = _unarmed;
                    if (activeHero.TryGetComponent(out IWeaponController weaponController))
                    {
                        weapon = weaponController.GetWeapon();
                    }
                    if (activeHero.TryGetComponent(out IAnimationMovementController animationController))
                    {
                        animationController.Animate(weapon.WeaponType.RunAnimationClipName);
                    }
                    await mapMovementController.DoMovement(path);
                    animationController.Animate(weapon.WeaponType.IdleAnimationClipName);

                    Hex lastHex = hexes[0];

                    mapUnitController.SetHexCoordinates(lastHex.Q, lastHex.R);
                    _nextTurnEvent.Raise();
                }
            }
        }

        private void NextTurn()
        {
            int heroIndex = _heroIndex++ % _heroes.Items.Count;
            _tiles.ClearSelectedList();

            GameObject activeHero = _heroes.Items[heroIndex];
            _activeMapHero.SetActiveGameObject(activeHero);
            _canWalkTiles = new List<ITile>();

            if (activeHero.TryGetComponent(out IMapUnitController mapUnitController))
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
                            _canWalkTiles.Add(neighborTile);
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
    }
}