using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class TavernHeroGenerator : MonoBehaviour
    {
        [SerializeField] 
        private HeroRuntimeSet tavernRoster;

        [SerializeField] 
        private GameEvent addHeroToTavern;

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

        private void OnEnable()
        {
            addHeroToTavern.OnRaise += AddHeroToTavern;
        }

        private void OnDisable()
        {
            addHeroToTavern.OnRaise -= AddHeroToTavern;
        }

        private void AddHeroToTavern()
        {
            Hero hero = CreateNewHero();
            tavernRoster.Add(hero);
        }

        public Hero CreateNewHero()
        {
            bool isMale = Random.Range(0, 2) == 0;
            string heroName = isMale ? _maleNames.GetRandomHeroName() : _femaleNames.GetRandomHeroName();
            Hero hero = new Hero(isMale, heroName, GenerateRandomStats());

            return hero;
        }

        private List<int> GenerateRandomStats()
        {
            List<int> heroStats = new List<int>();
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
    }
}