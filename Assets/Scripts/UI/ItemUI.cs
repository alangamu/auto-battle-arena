using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class ItemUI : MonoBehaviour, IItemUI
    {
        public bool IsInventory { get; set; }

        [SerializeField]
        private Image itemIcon;
        [SerializeField]
        private TMP_Text itemNameText;
        [SerializeField]
        private TMP_Text itemAmountText;
        [SerializeField]
        private DatabaseItem databaseItem;
        [SerializeField]
        private GameObject amountGameObject;

        private Item _item;

        public Item ItemRef => _item;

        public void Clear()
        {
            _item = null;
        }

        public void Setup(Item item)
        {
            _item = item;
            ItemSO itemSO = databaseItem.GetItem(_item);
            itemIcon.sprite = itemSO.ItemSprite;
            itemNameText.text = itemSO.name;

            if (_item.Amount == 1)
            {
                amountGameObject.SetActive(false);
                return;
            }
            amountGameObject.SetActive(true);
            itemAmountText.text = _item.Amount.ToString();
        }
    }
}