using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "Items/ItemManagerSO")]
    public class ItemManagerSO : ScriptableObject
    {
        public Weapon CreateWeapon(WeaponSO weapon, ItemRaritySO itemRarity)
        {
            return new Weapon(weapon, itemRarity);
        }

        public ArmorItem CreateWeareable(ArmorSO weareable, ItemRaritySO itemRarity)
        {
            return new ArmorItem(weareable, itemRarity);
        }

        public Item CreateMaterial(ItemSO item)
        {
            return new Item(item);
        }
    }
}