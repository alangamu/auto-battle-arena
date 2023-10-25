using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using HighlightPlus;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapTurnsMovementController : MonoBehaviour
    {
        [SerializeField]
        private ActiveHeroSO _activeHero;
        [SerializeField]
        private GameObjectVariable _activeMapHero;
        [SerializeField]
        private GameObjectGameEvent _selectTileEvent;
        [SerializeField]
        private GameObjectGameEvent _tileShowEnemyEvent;
        [SerializeField]
        private GameObjectRuntimeSet _heroes;
        [SerializeField]
        private GameEvent _nextTurnEvent;
        [SerializeField]
        private TileRuntimeSet _tiles;
        [SerializeField]
        private HeroStatSO _movementStat;
        [SerializeField]
        private WeaponSO _unarmed;
        [SerializeField]
        private Transform _mapMovementTransform;
        [SerializeField]
        private Transform _mapTransform;
        [SerializeField]
        private TileTypeSO _enemyTileType;
        [SerializeField]
        private TileTypeSO _heroTyleType;
        [SerializeField]
        private TileTypeSO _freeTyleType;
        [SerializeField]
        private TileTypeSO _cityTyleType;
        [SerializeField]
        private IntVariable _activeHeroIndex;

        private List<ITile> _canWalkTiles;
        private TileTypeSO _activeTileType;

        private void Awake()
        {
            _canWalkTiles = new List<ITile>();
        }

        private void OnEnable()
        {
            _nextTurnEvent.OnRaise += NextTurn;
            _selectTileEvent.OnRaise += SelectDestinationTileEvent;
        }

        private void OnDisable()
        {
            _nextTurnEvent.OnRaise -= NextTurn;
            _selectTileEvent.OnRaise -= SelectDestinationTileEvent;
        }

        private void SelectDestinationTileEvent(GameObject obj)
        {
            if (obj.TryGetComponent(out ITile tile))
            {
                if (_canWalkTiles != null)
                {
                    if (!_canWalkTiles.Contains(tile))
                    {
                        return;
                    }
                    _activeTileType = tile.TileType;
                    _ = MakeMovementAsync(tile.GetHex());
                }
            } 
        }

        public async Task MakeMovementAsync(Hex destinationHex)
        {
            GameObject activeHero = _activeMapHero.Value;
            if (activeHero.TryGetComponent(out IMapMovementController mapMovementController))
            {
                if (activeHero.TryGetComponent(out IMapUnitController mapUnitController))
                {
                    ITile startTile = _tiles.GetTileAt(mapUnitController.Q, mapUnitController.R);
                    Hex startHex = startTile.GetHex();
                    List<ITile> path = new List<ITile>();
                    List<Hex> hexes = _tiles.FindPath(startHex, destinationHex);

                    for (int i = hexes.Count - 1; i >= 0; i--)
                    {
                        path.Add(_tiles.GetTileAt(hexes[i].Q, hexes[i].R));
                    }

                    WeaponSO weapon = _unarmed;
                    if (activeHero.TryGetComponent(out IWeaponController weaponController))
                    {
                        weapon = weaponController.GetWeapon();
                    }
                    if (_activeTileType == _enemyTileType)
                    {
                        path.RemoveAt(path.Count - 1);
                    }
                    if (path.Count != 0)
                    {
                        if (activeHero.TryGetComponent(out IAnimationMovementController animationController))
                        {
                            animationController.Animate(weapon.WeaponType.RunAnimationClipName);
                        }
                        await mapMovementController.DoMovement(path);
                        animationController.Animate(weapon.WeaponType.IdleAnimationClipName);
                        ITile lastTyle = path[^1];
                        lastTyle.SetType(_heroTyleType);
                        activeHero.transform.SetParent(lastTyle.GetGameObject().transform);
                        if (activeHero.TryGetComponent(out IHeroController heroController))
                        {
                            heroController.ThisHero.MapPositionQ = lastTyle.GetHex().Q;
                            heroController.ThisHero.MapPositionR = lastTyle.GetHex().R;
                        }
                        if (lastTyle.TileType != _cityTyleType)
                        {
                            startTile.SetType(_freeTyleType);
                        }
                    }

                    if (_activeTileType == _enemyTileType)
                    {
                        ITile enemyTile = _tiles.GetTileAt(destinationHex.Q, destinationHex.R);
                        _tileShowEnemyEvent.Raise(enemyTile.GetGameObject());
                        //TODO: make the active enemy tile
                        return;
                    }

                    _nextTurnEvent.Raise();
                }
            }
        }

        private void NextTurn()
        {

            int heroIndex = _activeHeroIndex.Value;
            if (heroIndex == _heroes.Items.Count - 1)
            {
                heroIndex = 0;
            }
            else
            {
                heroIndex++;
            }

            _activeHeroIndex.SetValue(heroIndex);
            GameObject activeHero = _heroes.Items[_activeHeroIndex.Value];

            _activeMapHero.SetActiveGameObject(activeHero);

            foreach (var canWalkTile in _canWalkTiles)
            {
                canWalkTile.GetGameObject().transform.SetParent(_mapTransform);
            }

            _canWalkTiles.Clear();

            if (activeHero.TryGetComponent(out IMapUnitController mapUnitController))
            {
                Hex activeHeroHex = _tiles.GetHexAt(mapUnitController.Q, mapUnitController.R);
                int movementPoints = 0; 

                if (_activeMapHero.Value.TryGetComponent(out IHeroController heroController))
                {
                    movementPoints = heroController.ThisHero.ThisCombatStats.StatCount(_movementStat.StatId);
                    _activeHero.SetHero(heroController.ThisHero);
                }

                List<ITile> neighborTiles = _tiles.GetNeighboursTiles(activeHeroHex, movementPoints);

                foreach (var neighborTile in neighborTiles)
                {
                    if (neighborTile.GetHex().IsWalkable)
                    {
                        if (_tiles.FindPath(activeHeroHex, neighborTile.GetHex()).Count <= movementPoints)
                        {
                            _canWalkTiles.Add(neighborTile);
                            neighborTile.GetGameObject().transform.SetParent(_mapMovementTransform);
                        }
                    }
                }

                if (_mapMovementTransform.gameObject.TryGetComponent(out HighlightEffect highlight))
                {
                    highlight.Refresh();
                }
            }
        }
    }
}