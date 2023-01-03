using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class InventoryItemCountUI : MonoBehaviour
    {
        [SerializeField]
        private IntVariable maxInventorySlots;
        [SerializeField]
        private Inventory inventory;
        [SerializeField]
        private TMP_Text itemCountText;
        [SerializeField]
        private ItemEvent addItemToInventory;
        [SerializeField]
        private ItemEvent removeItemToInventory;

        private void OnEnable()
        {
            addItemToInventory.OnRaise += AddItemToInventory_OnRaise;
            removeItemToInventory.OnRaise += RemoveItemToInventory_OnRaise;
            itemCountText.text = $"{inventory.Items.Count} <#6f89a6>/ {maxInventorySlots.Value}";
        }

        private void RemoveItemToInventory_OnRaise(Item item)
        {
            itemCountText.text = $"{inventory.Items.Count} <#6f89a6>/ {maxInventorySlots.Value}";
        }

        private void AddItemToInventory_OnRaise(Item item)
        {
            itemCountText.text = $"{inventory.Items.Count} <#6f89a6>/ {maxInventorySlots.Value}";
        }
    }
}