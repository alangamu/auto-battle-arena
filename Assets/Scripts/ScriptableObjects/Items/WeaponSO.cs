using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "Items/WeaponSO")]
    public class WeaponSO : CrafteableItemSO
    {
        public GameObject ItemPrefab;
        public WeaponTypeSO WeaponType;
        public GameObject ProjectilePrefab;

        private void OnEnable()
        {
            base.SetId();
        }
    }
}