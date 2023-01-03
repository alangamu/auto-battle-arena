using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoFantasy.Scripts.UI
{
    public class ItemDropable : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private ItemTypeSO aceptedItemType;
        [SerializeField]
        private WeareableTypeSO wearableType;
        [SerializeField]
        private ItemTypeSO armorType;
        [SerializeField]
        private ItemEvent equipItem;
        [SerializeField]
        private ItemEvent removeItemFromInventory;
        [SerializeField]
        private GameEvent refreshUI;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                IItemUI itemType = eventData.pointerDrag.GetComponent<IItemUI>();

                if (itemType != null)
                {
                    Item item = itemType.ItemRef;
                    if (aceptedItemType.ItemTypeId == item.ItemTypeId)
                    {
                        if (aceptedItemType == armorType)
                        {
                            if (wearableType.WeareableTypeId == item.ItemWeareableTypeId)
                            {
                                Drop(item);
                            }
                            return;
                        }

                        Drop(item);
                    }
                }
            }
        }

        private void Drop(Item item)
        {
            if (TryGetComponent(out IItemUI thisItemUI))
            {
                thisItemUI.Setup(item);
                removeItemFromInventory.Raise(item);
                //TODO: see if this is still needed
                refreshUI.Raise();
            }
            equipItem.Raise(item);
        }
    }
}