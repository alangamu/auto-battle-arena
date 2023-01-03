using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    //TODO: is necessary this IItemUI?
    public class InventorySlotUI : MonoBehaviour, IItemUI
    {
        public bool IsInventory { get; set; }

        [SerializeField]
        private GameObject itemUIPrefab;

        private Item _item;
        private GameObject _itemUI;

        public Item ItemRef => _item;

        private void Awake()
        {
            Clear();
        }

        public void Clear()
        {
            _item = null;
            Destroy(_itemUI);
        }

        public void Setup(Item itemSO)
        {
            Clear();
            _item = itemSO;
            _itemUI = Instantiate(itemUIPrefab, transform);
            if (_itemUI.TryGetComponent(out IItemUI newItem))
            {
                newItem.Setup(itemSO);
                newItem.IsInventory = true;
            }
        }
    }
}