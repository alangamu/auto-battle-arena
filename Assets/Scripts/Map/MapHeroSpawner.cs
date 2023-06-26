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
        private CitySO _heroSpawnCity;
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
            ITile spawnCityTile = _tiles.GetTileAt(_heroSpawnCity.Q, _heroSpawnCity.R);
            GameObject tileGO = spawnCityTile.GetGameObject();
            Transform spwanCityTransform = tileGO.transform;
            foreach (var item in _heroes.Items)
            {
                GameObject heroMap = Instantiate(_heroMapPrefab, spwanCityTransform);

                if (heroMap.TryGetComponent(out IHeroController heroController))
                {
                    heroController.SetHero(item);
                    _activeMapHero.SetActiveGameObject(heroMap);
                    _activeHeroStandingTile.SetActiveGameObject(tileGO);
                }

                var _heroWeaponItem = item.ThisHeroData.HeroInventory.Find(x => x.ItemTypeId == _weaponType.ItemTypeId);
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
                    mapUnitController.SetHexCoordinates(_heroSpawnCity.Q, _heroSpawnCity.R);
                }
            }
        }
    }
}