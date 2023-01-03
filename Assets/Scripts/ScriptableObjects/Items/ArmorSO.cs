using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "Items/ArmorSO")]
    public class ArmorSO : CrafteableItemSO
    {
        public int PrefabIndexOnHero;
        public ArmorTypeSO ArmorType;

        private void OnEnable()
        {
            base.SetId();
        }
    }
}