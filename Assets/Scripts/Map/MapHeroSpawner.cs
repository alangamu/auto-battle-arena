using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapHeroSpawner : MonoBehaviour
    {
        [SerializeField]
        private TileRuntimeSet _tiles;
        [SerializeField]
        private GameEvent _spawnMapHeroesEvent;
        [SerializeField]
        private HeroRuntimeSet _heroes;
        [SerializeField]
        private GameObject _heroMapPrefab;
        [SerializeField]
        private ItemTypeSO _weaponType;
        [SerializeField]
        private WeaponSO _unarmed;
        [SerializeField]
        private ItemDatabase _databaseItem;
        [SerializeField]
        private GameObjectVariable _activeMapHero;
        [SerializeField]
        private GameObjectVariable _activeHeroStandingTile;
        [SerializeField]
        private TileTypeSO _cityTileType;
        [SerializeField]
        private TileTypeSO _heroTileType;

        private void OnEnable()
        {
            _spawnMapHeroesEvent.OnRaise += SpawnHeroes;
        }

        private void OnDisable()
        {
            _spawnMapHeroesEvent.OnRaise -= SpawnHeroes;
        }

        private void SpawnHeroes()
        {
            foreach (var item in _heroes.Items)
            {
                ITile spawnHeroTile = _tiles.GetTileAt(item.MapPositionQ, item.MapPositionR);
                GameObject tileGO = spawnHeroTile.GetGameObject();
                Transform spwanTileTransform = tileGO.transform;

                GameObject heroMap = Instantiate(_heroMapPrefab, spwanTileTransform);

                if (heroMap.TryGetComponent(out IHeroController heroController))
                {
                    heroController.SetHero(item);
                    _activeMapHero.SetActiveGameObject(heroMap);
                    _activeHeroStandingTile.SetActiveGameObject(tileGO);
                }

                var _heroWeaponItem = item.HeroInventory.Find(x => x.ItemTypeId == _weaponType.ItemTypeId);
                WeaponSO heroWeapon = _heroWeaponItem == null ? _unarmed : _databaseItem.GetItem(_heroWeaponItem) as WeaponSO;

                if (heroMap.TryGetComponent(out IWeaponController weaponController))
                {
                    weaponController.ShowWeapon(heroWeapon);
                }
                if (heroMap.TryGetComponent(out IAnimationMovementController animationMovementController))
                {
                    animationMovementController.Animate(heroWeapon.WeaponType.IdleAnimationClipName);
                }
                if (heroMap.TryGetComponent(out IMapUnitController mapUnitController))
                {
                    mapUnitController.SetHexCoordinates(item.MapPositionQ, item.MapPositionR);
                }

                if (spawnHeroTile.TileType != _cityTileType)
                {
                    spawnHeroTile.SetType(_heroTileType);
                }
            }
        }
    }
}