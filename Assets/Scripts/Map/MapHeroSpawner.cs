using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
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
        private GameEvent _heroesReadyEvent;
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
        private TileTypeSO _cityTileType;
        [SerializeField]
        private TileTypeSO _heroTileType;
        [SerializeField]
        private CitySO _startingCity;

        private void OnEnable()
        {
            _spawnMapHeroesEvent.OnRaise += SpawnHeroes;
            _heroes.OnAdd += SpawnNewHero;
        }

        private void OnDisable()
        {
            _spawnMapHeroesEvent.OnRaise -= SpawnHeroes;
            _heroes.OnAdd -= SpawnNewHero;
        }

        private void SpawnNewHero(Hero hero)
        {
            hero.MapPositionQ = _startingCity.Q;
            hero.MapPositionR = _startingCity.R;
            SpawnHero(hero);
        }

        private void SpawnHero(Hero hero)
        {
            ITile spawnHeroTile = _tiles.GetTileAt(hero.MapPositionQ, hero.MapPositionR);
            GameObject tileGO = spawnHeroTile.GetGameObject();
            Transform spwanTileTransform = tileGO.transform;

            GameObject heroMap = Instantiate(_heroMapPrefab, spwanTileTransform);

            if (heroMap.TryGetComponent(out IHeroController heroController))
            {
                heroController.SetHero(hero);
            }

            var _heroWeaponItem = hero.HeroInventory.Find(x => x.ItemTypeId == _weaponType.ItemTypeId);
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
                mapUnitController.SetHexCoordinates(hero.MapPositionQ, hero.MapPositionR);
            }

            if (spawnHeroTile.TileType != _cityTileType)
            {
                spawnHeroTile.SetType(_heroTileType);
            }
        }

        private void SpawnHeroes()
        {
            foreach (var item in _heroes.Items)
            {
                SpawnHero(item);
            }

            _heroesReadyEvent.Raise();
        }
    }
}