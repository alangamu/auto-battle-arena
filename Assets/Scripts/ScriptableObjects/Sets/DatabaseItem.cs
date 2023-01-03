using AutoFantasy.Scripts.ScriptableObjects.Items;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(fileName = "DatabaseItem", menuName = "Database/Item Database")]
    public class DatabaseItem : Database<ItemSO>
    {
        [SerializeField]
        private ItemTypeSO weaponTypeItem;
        [SerializeField]
        private ItemTypeSO armorTypeItem;
        [SerializeField]
        private ItemTypeSO materialTypeItem;

        public ItemSO GetRandomArmorByType(ArmorTypeSO armorType)
        {
            List<ItemSO> armors = Items.FindAll(x => x.ItemType == armorTypeItem);
            List<ArmorSO> armorsSO = armors.ConvertAll(x => (ArmorSO)x).FindAll(j => j.ArmorType == armorType);

            int randomIndex = Random.Range(0, armorsSO.Count);

            return armorsSO[randomIndex];
        }

        public WeaponSO GetRandomWeaponByType(WeaponTypeSO weaponType)
        {
            List<ItemSO> weapons = Items.FindAll(x => x.ItemType == weaponTypeItem);
            List<WeaponSO> weaponsSO = weapons.ConvertAll(x => (WeaponSO)x).FindAll(j => j.WeaponType == weaponType);

            int randomIndex = Random.Range(0, weaponsSO.Count);

            return weaponsSO[randomIndex];
        }

        public ItemSO GetRandomMaterial(ItemTypeSO materialTypeItem)
        {
            List<ItemSO> items = Items.FindAll(x => x.ItemType == materialTypeItem);

            int randomIndex = Random.Range(0, items.Count);

            return items[randomIndex];
        }

        public ItemSO GetItem(Item item)
        {
            return Items.Find(x => x.ItemId == item.ItemRefId);
        }
    }
}