using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(menuName = "Database/Inventory")]
    public class Inventory : Database<Item>
    {
        [SerializeField]
        private ItemEvent addItemToInventory;
        [SerializeField]
        private ItemEvent removeItemFromInventory;
        [SerializeField]
        private ItemTypeSO goldType;
        [SerializeField]
        private IntVariable gold; 

        public int GetItemAmount(ItemSO item)
        {
            if (item.ItemType == goldType)
            {
                return gold.Value;
            }

            Item inventoryItem = Items.Find(x => x.ItemRefId == item.ItemId);
            return inventoryItem == null ? 0 : inventoryItem.Amount;
        }

        private void OnEnable()
        {
            addItemToInventory.OnRaise += AddItemToInventory_OnRaise;
            removeItemFromInventory.OnRaise += RemoveItemToInventory_OnRaise;
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        private void OnDisable()
        {
            addItemToInventory.OnRaise -= AddItemToInventory_OnRaise;
            removeItemFromInventory.OnRaise -= RemoveItemToInventory_OnRaise;
        }

        private void RemoveItemToInventory_OnRaise(Item item)
        {
            if (item.ItemTypeId == goldType.ItemTypeId)
            {
                gold.ApplyChange(-item.Amount);
                return;
            }

            if (item.IsStackable)
            {
                Item inventoryItem = Items.Find(x => x.ItemRefId == item.ItemRefId);
                if (inventoryItem != null)
                {
                    inventoryItem.Amount -= item.Amount;
                    if (inventoryItem.Amount == 0)
                    {
                        Items.Remove(inventoryItem);
                    }
                    return;
                }
            }

            //TODO: wen start play and the inventary has a Weapon, it turns into a Item. Maybe load on play be a good solution
            Items.Remove(item);
        }

        private void AddItemToInventory_OnRaise(Item item)
        {
            if (item.ItemTypeId == goldType.ItemTypeId)
            {
                gold.ApplyChange(item.Amount);
                return;
            }

            if (item.IsStackable)
            {
                Item inventoryItem = Items.Find(x => x.ItemRefId == item.ItemRefId);
                if (inventoryItem != null)
                {
                    inventoryItem.Amount += item.Amount;
                    return;
                }
            }

            Items.Add(item);
        }
    }
}