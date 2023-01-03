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
        private DatabaseName maleNames;
        [SerializeField]
        private DatabaseName femaleNames;

        [SerializeField]
        private IntVariable heroBasePoints;

        [SerializeField] 
        private HeroRuntimeSet tavernRoster;

        [SerializeField] 
        private GameEvent addHeroToTavern;

        [SerializeField] 
        private IntVariable tavernMaxCapacity;

        [SerializeField]
        private HeroStatSO healthStat;
        [SerializeField]
        private HeroStatSO defenseStat;
        [SerializeField]
        private HeroStatSO criticalChanceStat;
        [SerializeField]
        private HeroStatSO criticalPowerStat;
        [SerializeField]
        private HeroStatSO attackSpeedStat;
        [SerializeField]
        private HeroStatSO attackPowerStat;

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
            bool isMale = Random.Range(0, 2) == 0;
            string heroName = isMale ? maleNames.GetRandomHeroName() : femaleNames.GetRandomHeroName();
            Hero hero = new Hero(isMale, heroName, GenerateRandomStats());
            tavernRoster.Add(hero);
        }

        private List<int> GenerateRandomStats()
        {
            List<int> heroStats = new List<int>();
            for (int i = 0; i < heroBasePoints.Value; i++)
            {
                int randomNumber = Random.Range(0, 6);
                switch (randomNumber)
                {
                    case 0:
                        heroStats.Add(healthStat.StatId);
                        break;
                    case 1:
                        heroStats.Add(defenseStat.StatId);
                        break;
                    case 2:
                        heroStats.Add(criticalChanceStat.StatId);
                        break;
                    case 3:
                        heroStats.Add(criticalPowerStat.StatId);
                        break;
                    case 4:
                        heroStats.Add(attackSpeedStat.StatId);
                        break;
                    case 5:
                        heroStats.Add(attackPowerStat.StatId);
                        break;
                    default:
                        break;
                }
            }

            return heroStats;
        }
    }
}