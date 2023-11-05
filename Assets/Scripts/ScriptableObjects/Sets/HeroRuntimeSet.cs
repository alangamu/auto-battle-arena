using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(fileName = "HeroRuntimeSet", menuName = "Sets/HeroRuntimeSet", order = 0)]
    public class HeroRuntimeSet : RuntimeSet<Hero>
    {
        [SerializeField]
        private DatabaseName _maleNames;
        [SerializeField]
        private DatabaseName _femaleNames;

        [SerializeField]
        private IntVariable _heroBasePoints;

        [SerializeField]
        private HeroStatSO _healthStat;
        [SerializeField]
        private HeroStatSO _defenseStat;
        [SerializeField]
        private HeroStatSO _criticalChanceStat;
        [SerializeField]
        private HeroStatSO _criticalPowerStat;
        [SerializeField]
        private HeroStatSO _attackSpeedStat;
        [SerializeField]
        private HeroStatSO _attackPowerStat;
        [SerializeField]
        private HeroStatSO _sightStat;
        [SerializeField]
        private HeroStatSO _movementPointsStat;

        public void AddNewHero()
        {
            Hero hero = CreateNewHero();
            Add(hero);
        }

        private Hero CreateNewHero()
        {
            bool isMale = Random.Range(0, 2) == 0;
            string heroName = isMale ? _maleNames.GetRandomHeroName() : _femaleNames.GetRandomHeroName();
            Hero hero = new(isMale, heroName, GenerateRandomStats());
            //TODO: review this later
            hero.ThisCombatStats.Stats.Add(_sightStat.StatId);
            hero.ThisCombatStats.Stats.Add(_sightStat.StatId);
            hero.ThisCombatStats.Stats.Add(_movementPointsStat.StatId);
            hero.ThisCombatStats.Stats.Add(_movementPointsStat.StatId);
            return hero;
        }

        private List<int> GenerateRandomStats()
        {
            List<int> heroStats = new();
            for (int i = 0; i < _heroBasePoints.Value; i++)
            {
                int randomNumber = Random.Range(0, 6);
                switch (randomNumber)
                {
                    case 0:
                        heroStats.Add(_healthStat.StatId);
                        break;
                    case 1:
                        heroStats.Add(_defenseStat.StatId);
                        break;
                    case 2:
                        heroStats.Add(_criticalChanceStat.StatId);
                        break;
                    case 3:
                        heroStats.Add(_criticalPowerStat.StatId);
                        break;
                    case 4:
                        heroStats.Add(_attackSpeedStat.StatId);
                        break;
                    case 5:
                        heroStats.Add(_attackPowerStat.StatId);
                        break;
                    default:
                        break;
                }
            }

            return heroStats;
        }

        private void OnEnable()
        {
            foreach (var item in Items)
            {
                item.OnAddItem += HeroOnInventoryChanged;
            }
        }

        private void OnDisable()
        {
            foreach (var item in Items)
            {
                item.OnAddItem -= HeroOnInventoryChanged;
            }
        }

        private void HeroOnInventoryChanged(Item item)
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif            
        }
    }
}