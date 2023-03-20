using AutoFantasy.Scripts.ScriptableObjects.MovementTypes;
using AutoFantasy.Scripts.ScriptableObjects.Skills;
using System;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [Serializable]
    public class Weapon : Item
    {
        //public SkillSO Skill;

        public Weapon(WeaponSO itemSO, ItemRaritySO itemRarity) : base(itemSO, itemRarity.ItemRarityId, itemSO.WeaponType.WeareableTypeId)
        {
        }

        public void RandomizeStats(ItemRaritySO itemRarity, WeaponSO weaponSO)
        {
            for (int i = 0; i < itemRarity.WeaponFixedStatCount; i++)
            {
                HeroStatSO fixedStat = weaponSO.WeaponType.FixedStats[i];
                ItemStats.Add(fixedStat.StatId);
            }

            HeroStatSO[] heroStats = weaponSO.WeaponType.FixedStats;
            int statsLength = heroStats.Length;
            for (int i = 0; i < itemRarity.WeaponRandomStatCount; i++)
            {
                HeroStatSO randomStat = heroStats[UnityEngine.Random.Range(0, statsLength)];
                ItemStats.Add(randomStat.StatId);
            }
        }
    }
}