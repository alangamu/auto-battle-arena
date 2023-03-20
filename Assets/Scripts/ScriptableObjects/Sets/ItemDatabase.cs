using AutoFantasy.Scripts.ScriptableObjects.Items;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(fileName = "DatabaseItem", menuName = "Database/Item Database")]
    public class ItemDatabase : Database<ItemSO>
    {
        [SerializeField]
        private ItemTypeSO _weaponTypeItem;
        [SerializeField]
        private ItemTypeSO _armorTypeItem;
        [SerializeField]
        private SkillDatabase _databaseSkill;

        public Item CreateNewItem(ItemSO itemSO, ItemRaritySO itemRaritySO)
        {
            Item item = new Item(itemSO);
            if (itemSO.ItemType == _armorTypeItem)
            {
                item = CreateArmor(itemSO, itemRaritySO);
            }

            if (itemSO.ItemType == _weaponTypeItem)
            {
                item = CreateWeapon(itemSO, itemRaritySO);
            }

            return item;
        }

        public ItemSO GetRandomArmorByType(ArmorTypeSO armorType)
        {
            List<ItemSO> armors = Items.FindAll(x => x.ItemType == _armorTypeItem);
            List<ArmorSO> armorsSO = armors.ConvertAll(x => (ArmorSO)x).FindAll(j => j.ArmorType == armorType);

            int randomIndex = Random.Range(0, armorsSO.Count);

            return armorsSO[randomIndex];
        }

        public WeaponSO GetRandomWeaponByType(WeaponTypeSO weaponType)
        {
            List<ItemSO> weapons = Items.FindAll(x => x.ItemType == _weaponTypeItem);
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

        private Item CreateWeapon(ItemSO itemSO, ItemRaritySO itemRaritySO)
        {
            WeaponSO weaponSO = itemSO as WeaponSO;
            Weapon weapon = new Weapon(weaponSO, itemRaritySO);
            weapon.RandomizeStats(itemRaritySO, weaponSO);
            weapon.SkillId = _databaseSkill.GetRandomSkillByWeaponType(weaponSO.WeaponType).SkillId;
            return weapon;
        }

        private Item CreateArmor(ItemSO itemSO, ItemRaritySO itemRaritySO)
        {
            ArmorSO armorSO = itemSO as ArmorSO;
            ArmorItem armor = new ArmorItem(armorSO, itemRaritySO);
            armor.RandomizeStats(itemRaritySO, armorSO);
            return armor;
        }

        private void OnEnable()
        {
            Items = new List<ItemSO>();
            ItemSO[] itemsArray = Resources.LoadAll<ItemSO>("Items");
            foreach (var item in itemsArray)
            {
                Items.Add(item);
            }
            Debug.Log($"Loaded {itemsArray.Length} items");
        }
    }
}