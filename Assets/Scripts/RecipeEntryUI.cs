using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class RecipeEntryUI : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text itemAmountText;
        [SerializeField]
        private TMP_Text itemNameText;
        [SerializeField]
        private Inventory inventory;
        [SerializeField]
        private GameObject checkObject;

        public bool Setup(RecipeEntry recipeEntry)
        {
            ItemSO itemSO = recipeEntry.ItemSO;
            int itemAmount = inventory.GetItemAmount(itemSO);
            int itemRequiredAmount = recipeEntry.Amount;
            bool canCraft = itemAmount >= itemRequiredAmount;

            itemImage.sprite = itemSO.ItemSprite;
            itemNameText.text = itemSO.ItemName;
            itemAmountText.text = $"{itemAmount} <#6f89a6>/ {itemRequiredAmount}";
            checkObject.SetActive(canCraft);

            return canCraft;
        }
    }
}