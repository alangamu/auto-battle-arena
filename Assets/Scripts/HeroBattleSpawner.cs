using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
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
        private List<Transform> heroesTransforms;
        [SerializeField] 
        private GameEvent spawnHeroes;
        [SerializeField] 
        private GameObject heroPrefab;
        [SerializeField]
        private GameObjectGameEvent heroSetEnemyEvent;
        [SerializeField]
        private TeamsSO teams;
        [SerializeField]
        private HeroRuntimeSet heroes;
        [SerializeField]
        private DatabaseItem databaseItem;
        [SerializeField]
        private WeaponTypeSO unarmed;
        [SerializeField]
        private ItemTypeSO weaponType;

        private void OnEnable()
        {
            spawnHeroes.OnRaise += SpawnHeroes_OnRaise;
        }

        private void OnDisable()
        {
            spawnHeroes.OnRaise -= SpawnHeroes_OnRaise;
        }

        private void SpawnHeroes_OnRaise()
        {
            List<string> heroesToSpawn = teams.Teams[teams.ActiveTeamNumber].HeroesIds;

            for (int i = 0; i < heroesToSpawn.Count; i++)
            {
                Transform spawnPoint = heroesTransforms[i];
                GameObject hero = Instantiate(heroPrefab, spawnPoint.position, spawnPoint.rotation, transform);

                Hero hetoToSpawn = heroes.Items.Find(x => x.GetHeroId() == heroesToSpawn[i]);

                if (hero.TryGetComponent(out IHeroController heroController))
                {
                    heroController.SetHero(hetoToSpawn);
                }

                //heroSetEvent.Raise(hero, hetoToSpawn);
                heroSetEnemyEvent.Raise(hero);

                if (hero.TryGetComponent(out ICombatController combatController))
                {
                    combatController.SetCombatStats(hetoToSpawn.ThisCombatStats);
                }
                if (hero.TryGetComponent(out IAnimationController animationController))
                {
                    var heroInventory = hetoToSpawn.ThisHeroData.HeroInventory;
                    WeaponSO weapon = null;
                    foreach (var item in heroInventory)
                    {
                        var itemInventory = databaseItem.GetItem(item);
                        if (itemInventory.ItemType == weaponType)
                        {
                            weapon = (WeaponSO)itemInventory;
                            continue;
                        }
                    }

                    if (weapon == null)
                    {
                        animationController.SetWeaponType(unarmed);
                        continue;
                    }
                    animationController.SetWeaponType(weapon.WeaponType);
                }
            }
        }
    }
}