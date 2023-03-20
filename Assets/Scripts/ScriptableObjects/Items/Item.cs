using System;
using System.Collections.Generic;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [Serializable]
    public class Item 
    {
        public int ItemRarityId;
        public string ItemRefId;
        public int ItemTypeId;
        public int ItemWeareableTypeId;
        public int Amount;
        public bool IsStackable;
        public string SkillId;

        public List<int> ItemStats;

        public Item(ItemSO itemSO, int itemRarityId, int itemWeareableTypeId)
        {
            ItemRarityId = itemRarityId;
            ItemWeareableTypeId = itemWeareableTypeId;
            IsStackable = false;
            ItemRefId = itemSO.ItemId;
            ItemTypeId = itemSO.ItemType.ItemTypeId;
            ItemStats = new List<int>();
            Amount = 1;
        }

        public Item(ItemSO itemSO)
        {
            ItemRefId = itemSO.ItemId;
            ItemTypeId = itemSO.ItemType.ItemTypeId;
            IsStackable = itemSO.IsStackable;
            ItemStats = new List<int>();
            Amount = 1;
        }

        public void SetAmount(int amount)
        {
            Amount = amount;
        }

        public void AddAmount(int amount)
        {
            Amount += amount;
        }
    }
}