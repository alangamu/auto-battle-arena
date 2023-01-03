using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class InventoryTest : MonoBehaviour
    {
        [SerializeField]
        private ItemEvent addItemToInventory;

        [SerializeField]
        private DatabaseItem itemDatabase;
        [SerializeField]
        private ItemManagerSO itemManager;

        [SerializeField]
        private ItemRaritySO normalRarity;
        [SerializeField]
        private ItemRaritySO rareRarity;
        [SerializeField]
        private ItemRaritySO eliteRarity;

        [SerializeField]
        private ArmorTypeSO bootTypeItem;
        [SerializeField]
        private ArmorTypeSO glovesTypeItem;
        [SerializeField]
        private ArmorTypeSO chestTypeItem;
        [SerializeField]
        private ArmorTypeSO pantsTypeItem;
        [SerializeField]
        private ArmorTypeSO bracersTypeItem;
        [SerializeField]
        private ArmorTypeSO shouldersTypeItem;
        [SerializeField]
        private ArmorTypeSO _helmetTypeItem;

        [SerializeField]
        private WeaponTypeSO sword2HandWeaponType;
        [SerializeField]
        private WeaponTypeSO daggerWeaponType;
        [SerializeField]
        private WeaponTypeSO sword1HandWeaponType;
        [SerializeField]
        private WeaponTypeSO bowWeaponType;
        [SerializeField]
        private WeaponTypeSO spear2HandWeaponType;
        [SerializeField]
        private WeaponTypeSO mace1HandWeaponType;
        [SerializeField]
        private WeaponTypeSO spear1HandWeaponType;
        //[SerializeField]
        //private WeaponTypeSO crossbow2HandWeaponType;
        [SerializeField]
        private WeaponTypeSO staff2HandWeaponType;

        [SerializeField]
        private ItemTypeSO materialType;

        private void OnGUI()
        {
            if (GUILayout.Button("Create random Material"))
            {
                AddMaterialToInventory(materialType);
            }
            if (GUILayout.Button("Create normal 1 hand sword"))
            {
                AddWeaponToInventory(normalRarity, sword1HandWeaponType);
            }
            if (GUILayout.Button("Create normal 2 hand bow"))
            {
                AddWeaponToInventory(normalRarity, bowWeaponType);
            }
            if (GUILayout.Button("Create normal 2 hand spear"))
            {
                AddWeaponToInventory(normalRarity, spear2HandWeaponType);
            }
            if (GUILayout.Button("Create normal 1 hand mace"))
            {
                AddWeaponToInventory(normalRarity, mace1HandWeaponType);
            }
            if (GUILayout.Button("Create normal 1 hand spear"))
            {
                AddWeaponToInventory(normalRarity, spear1HandWeaponType);
            }
            //if (GUILayout.Button("Create normal 2 hand crossbow"))
            //{
            //    AddWeaponToInventory(normalRarity, crossbow2HandWeaponType);
            //}
            if (GUILayout.Button("Create normal 2 hand sword"))
            {
                AddWeaponToInventory(normalRarity, sword2HandWeaponType);
            }
            if (GUILayout.Button("Create normal 2 hand staff"))
            {
                AddWeaponToInventory(normalRarity, staff2HandWeaponType);
            }
            if (GUILayout.Button("Create normal dagger"))
            {
                AddWeaponToInventory(normalRarity, daggerWeaponType);
            }
            if (GUILayout.Button("Create normal boots"))
            {
                AddItemToInventory(normalRarity, bootTypeItem);
            }
            if (GUILayout.Button("Create normal gloves"))
            {
                AddItemToInventory(normalRarity, glovesTypeItem);
            }
            if (GUILayout.Button("Create normal chest"))
            {
                AddItemToInventory(normalRarity, chestTypeItem);
            }
            if (GUILayout.Button("Create normal pants"))
            {
                AddItemToInventory(normalRarity, pantsTypeItem);
            }
            if (GUILayout.Button("Create normal bracers"))
            {
                AddItemToInventory(normalRarity, bracersTypeItem);
            }
            if (GUILayout.Button("Create normal shoulders"))
            {
                AddItemToInventory(normalRarity, shouldersTypeItem);
            }
            if (GUILayout.Button("Create normal helmet"))
            {
                AddItemToInventory(normalRarity, _helmetTypeItem);
            }
        }

        private void AddItemToInventory(ItemRaritySO itemRarity, ArmorTypeSO itemType)
        {
            ItemSO itemSO = itemDatabase.GetRandomArmorByType(itemType);
            ArmorSO weareableSO = (ArmorSO)itemSO;
            ArmorItem weareableItem = itemManager.CreateWeareable(weareableSO, itemRarity);
            weareableItem.RandomizeStats(itemRarity, weareableSO);

            addItemToInventory.Raise(weareableItem);
        }

        private void AddWeaponToInventory(ItemRaritySO itemRarity, WeaponTypeSO weaponType)
        {
            ItemSO itemSO = itemDatabase.GetRandomWeaponByType(weaponType);
            WeaponSO weaponSO = (WeaponSO)itemSO;
            Weapon weaponItem = itemManager.CreateWeapon(weaponSO, itemRarity);
            weaponItem.RandomizeStats(itemRarity, weaponSO);

            addItemToInventory.Raise(weaponItem);
        }

        private void AddMaterialToInventory(ItemTypeSO materialType)
        {
            ItemSO itemSO = itemDatabase.GetRandomMaterial(materialType);

            Item item = itemManager.CreateMaterial(itemSO);

            addItemToInventory.Raise(item);
        }

    }
}