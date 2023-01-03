using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class RecipeDetailsUI : MonoBehaviour
    {
        [SerializeField]
        private Transform parentTransform;
        [SerializeField]
        private RecipeEntryUI recipeEntryUIPrefab;
        [SerializeField]
        private ItemSOEvent selectItemEvent;
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text itemNameText;
        [SerializeField]
        private Button craftButton;
        [SerializeField]
        private GameObject craftButtonObject;
        [SerializeField]
        private GameObject craftButtonDisabledObject;
        [SerializeField]
        private ItemEvent addItemToInventory;
        [SerializeField]
        private ItemEvent removeItemFromInventory;

        private bool _canCraft;
        private List<RecipeEntry> _recipeEntries;
        private ItemSO _itemSO;

        private void OnEnable()
        {
            selectItemEvent.OnRaise += ItemEvent_OnRaise;
            craftButton.onClick.AddListener(CraftItem);
        }

        private void OnDisable()
        {
            selectItemEvent.OnRaise -= ItemEvent_OnRaise;
            craftButton.onClick.RemoveAllListeners();
        }

        private void CraftItem()
        {
            foreach (var recipeEntry in _recipeEntries)
            {
                Item item = new Item(recipeEntry.ItemSO);
                item.SetAmount(recipeEntry.Amount);
                removeItemFromInventory.Raise(item);
            }

            Item itemToAdd = new Item(_itemSO);
            addItemToInventory.Raise(itemToAdd);

            ItemEvent_OnRaise(_itemSO);
        }

        private void ItemEvent_OnRaise(ItemSO itemSO)
        {
            _itemSO = itemSO;
            _canCraft = true;
            itemImage.sprite = _itemSO.ItemSprite;
            itemNameText.text = _itemSO.ItemName;
            ClearContent();

            CrafteableItemSO crafteableItemSO = _itemSO as CrafteableItemSO;

            _recipeEntries = crafteableItemSO.RecipeIngredients;
            foreach (var recipeEntry in _recipeEntries)
            {
                RecipeEntryUI recipeEntryUI = Instantiate(recipeEntryUIPrefab, parentTransform);
                if (!recipeEntryUI.Setup(recipeEntry))
                {
                    _canCraft = false;
                }
            }

            HandleButton();
        }

        private void HandleButton()
        {
            craftButton.enabled = _canCraft;
            craftButtonObject.SetActive(_canCraft);
            craftButtonDisabledObject.SetActive(!_canCraft);
        }

        private void ClearContent()
        {
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}