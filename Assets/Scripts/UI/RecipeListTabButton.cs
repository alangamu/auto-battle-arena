using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class RecipeListTabButton : MonoBehaviour
    {
        [SerializeField]
        private Transform contentTransform;
        [SerializeField]
        private GameObject activeVisual;
        [SerializeField]
        private ItemTypeSO itemType;
        [SerializeField]
        private ItemTypeEvent itemTypeEvent;
        [SerializeField]
        private ItemDatabase itemDatabase;
        [SerializeField]
        private RecipeListItemUI recipeListItemUIPrefab;
        [SerializeField]
        private Button tabButton;
        [SerializeField]
        private Color activeColor;
        [SerializeField]
        private Color normalColor;
        [SerializeField]
        private Image icon;
        [SerializeField]
        private ItemSOEvent selectItemEvent;

        private void OnEnable()
        {
            itemTypeEvent.OnRaise += ItemTypeEvent_OnRaise;
            tabButton.onClick.AddListener(ItemTypeEventRaise);
        }

        private void OnDisable()
        {
            itemTypeEvent.OnRaise -= ItemTypeEvent_OnRaise;
        }

        private void ItemTypeEvent_OnRaise(ItemTypeSO eventItemType)
        {
            if (eventItemType == itemType)
            {
                Select();
                ClearContent();
                FillItemTypeRecipeList();
                return;
            }

            Deselect();
        }

        private void Deselect()
        {
            icon.color = normalColor;
            activeVisual.SetActive(false);
        }

        private void Select()
        {
            icon.color = activeColor;
            activeVisual.SetActive(true);
        }

        private void FillItemTypeRecipeList()
        {
            List<ItemSO> items = itemDatabase.Items.FindAll(x => x.ItemType == itemType);
            List<CrafteableItemSO> recipeItems = items.ConvertAll(x => (CrafteableItemSO)x);

            foreach (var item in recipeItems)
            {
                RecipeListItemUI recipeListItemUIObject = Instantiate(recipeListItemUIPrefab, contentTransform);
                recipeListItemUIObject.Setup(item);
            }

            selectItemEvent.Raise(recipeItems[0]);
        }

        private void ClearContent()
        {
            foreach (Transform child in contentTransform)
            {
                Destroy(child.gameObject);
            }
        }

        private void ItemTypeEventRaise()
        {
            itemTypeEvent.Raise(itemType);
        }
    }
}