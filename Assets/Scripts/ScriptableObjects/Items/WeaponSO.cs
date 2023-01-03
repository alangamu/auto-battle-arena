using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "Items/WeaponSO")]
    public class WeaponSO : CrafteableItemSO
    {
        public GameObject ItemPrefab;
        public WeaponTypeSO WeaponType;

        private void OnEnable()
        {
            base.SetId();
        }
    }
}