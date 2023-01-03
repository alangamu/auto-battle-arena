using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class RecipeListItemUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject selected;
        [SerializeField]
        private GameObject normal;
        [SerializeField]
        private TMP_Text itemNameText;
        [SerializeField]
        private Image itemIcon;
        [SerializeField]
        private ItemSOEvent selectItemEvent;
        [SerializeField]
        private Button selectItemButton;

        private ItemSO _itemSO;

        public void Setup(ItemSO item)
        {
            _itemSO = item;
            itemNameText.text = _itemSO.ItemName;
            itemIcon.sprite = _itemSO.ItemSprite;
        }

        private void OnEnable()
        {
            selectItemButton.onClick.AddListener(Clicked);
            selectItemEvent.OnRaise += SelectItemEvent_OnRaise;
        }

        private void OnDisable()
        {
            selectItemEvent.OnRaise -= SelectItemEvent_OnRaise;
        }

        private void SelectItemEvent_OnRaise(ItemSO itemSO)
        {
            if (_itemSO == itemSO)
            {
                selected.SetActive(true);
                normal.SetActive(false);
                return;
            }

            selected.SetActive(false);
            normal.SetActive(true);
        }

        private void Clicked()
        {
            selectItemEvent.Raise(_itemSO);
        }
    }
}