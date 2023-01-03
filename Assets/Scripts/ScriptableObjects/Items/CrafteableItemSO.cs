using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "Items/CrafteableItem")]
    public class CrafteableItemSO : ItemSO
    {
        public List<RecipeEntry> RecipeIngredients;

        private void OnEnable()
        {
            base.SetId();
        }
    }
}