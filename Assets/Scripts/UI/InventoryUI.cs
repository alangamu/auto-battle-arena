using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]
        private Transform parentTransform;
        [SerializeField]
        private GameObject inventorySlotUIPrefab;
        [SerializeField]
        private Inventory inventory;
        [SerializeField]
        private IntVariable maxInventorySlots;
        [SerializeField]
        private GameEvent refreshUI;
        [SerializeField]
        private ItemEvent addItemToInventory;
        [SerializeField]
        private ItemEvent removeItemToInventory;


        private void OnEnable()
        {
            refreshUI.OnRaise += RefreshUI_OnRaise;
            addItemToInventory.OnRaise += RefreshInventory;
            removeItemToInventory.OnRaise += RefreshInventory;
            FillInventory();
        }

        private void OnDisable()
        {
            refreshUI.OnRaise -= RefreshUI_OnRaise;
            addItemToInventory.OnRaise -= RefreshInventory;
            removeItemToInventory.OnRaise -= RefreshInventory;
        }

        private void RefreshInventory(Item obj)
        {
            FillInventory();
        }

        private void FillInventory()
        {
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < maxInventorySlots.Value; i++)
            {
                GameObject inventorySlotUIObject = Instantiate(inventorySlotUIPrefab, parentTransform);
                if (i < inventory.Items.Count)
                {
                    Item item = inventory.Items[i];
                    if (inventorySlotUIObject.TryGetComponent(out InventorySlotUI inventorySlotUI))
                    {
                        inventorySlotUI.Setup(item);
                    }
                }
            }
        }

        private void RefreshUI_OnRaise()
        {
            FillInventory();
        }
    }
}