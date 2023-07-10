using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class HeroBattleSpawner : MonoBehaviour
    {
        [SerializeField] 
        private List<Transform> _heroesTransforms;
        [SerializeField] 
        private GameEvent _spawnHeroes;
        [SerializeField] 
        private GameObject _heroPrefab;
        [SerializeField]
        private HeroRuntimeSet _fightHeroes;
        [SerializeField]
        private ItemDatabase _databaseItem;
        [SerializeField]
        private WeaponTypeSO _unarmed;
        [SerializeField]
        private ItemTypeSO _weaponType;

        private void OnEnable()
        {
            _spawnHeroes.OnRaise += SpawnHeroes_OnRaise;
        }

        private void OnDisable()
        {
            _spawnHeroes.OnRaise -= SpawnHeroes_OnRaise;
        }

        private void SpawnHeroes_OnRaise()
        {
            for (int i = 0; i < _fightHeroes.Items.Count; i++)
            {
                Transform spawnPoint = _heroesTransforms[i];
                GameObject hero = Instantiate(_heroPrefab, spawnPoint.position, spawnPoint.rotation, transform);

                Hero heroToSpawn = _fightHeroes.Items[i];

                hero.name = heroToSpawn.ThisHeroData.HeroName;

                //TODO: improve this weapon getting thing
                var heroInventory = heroToSpawn.HeroInventory;
                WeaponSO weapon = null;
                foreach (var item in heroInventory)
                {
                    var itemInventory = _databaseItem.GetItem(item);
                    if (itemInventory.ItemType == _weaponType)
                    {
                        weapon = (WeaponSO)itemInventory;
                        continue;
                    }
                }

                if (hero.TryGetComponent(out IHeroController heroController))
                {
                    heroController.SetHero(heroToSpawn);
                }

                if (hero.TryGetComponent(out ICombatController combatController))
                {
                    combatController.SetCombatStats(heroToSpawn.ThisCombatStats);
                    combatController.SetTeamIndex(i);
                }
                if (hero.TryGetComponent(out IAnimationController animationController))
                {
                    animationController.SetWeaponType(weapon == null ? _unarmed : weapon.WeaponType);
                }
            }
        }
    }
}