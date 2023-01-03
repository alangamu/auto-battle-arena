using System;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [Serializable]
    public class ArmorItem : Item
    {
        public ArmorItem(ArmorSO itemSO, ItemRaritySO itemRarity) : base(itemSO, itemRarity.ItemRarityId, itemSO.ArmorType.WeareableTypeId)
        {
        }

        public void RandomizeStats(ItemRaritySO itemRarity, ArmorSO weareableType)
        {
            for (int i = 0; i < itemRarity.ArmorFixedStatCount; i++)
            {
                HeroStatSO fixedStat = weareableType.ArmorType.FixedStats[i];
                ItemStats.Add(fixedStat.StatId);
            }

            HeroStatSO[] heroStats = weareableType.ArmorType.FixedStats;
            int statsLength = heroStats.Length;
            for (int i = 0; i < itemRarity.ArmorRandomStatCount; i++)
            {
                HeroStatSO randomStat = heroStats[UnityEngine.Random.Range(0, statsLength)];
                ItemStats.Add(randomStat.StatId);
            }
        }
    }
}