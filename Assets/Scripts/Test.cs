using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private GameEvent testEvent;
        [SerializeField]
        private ItemDatabase databaseItem;
        [SerializeField]
        private Item item;
        [SerializeField]
        private ItemTypeSO armorTypeItem;
        [SerializeField]
        private ItemTypeSO weaponTypeItem;


        private void OnEnable()
        {
            testEvent.OnRaise += TestEvent_OnRaise;
        }

        private void OnDisable()
        {
            testEvent.OnRaise += TestEvent_OnRaise;
        }

        private void TestEvent_OnRaise()
        {
            //GetItem();
            //Load();
            SetIdItems();
        }

        private void SetIdItems()
        {

        }

        private void GetItem()
        {
            List<ItemSO> armors = databaseItem.Items.FindAll(x => x.ItemType.ItemTypeId == item.ItemTypeId && x.ItemId == item.ItemRefId);

            if (item.ItemTypeId == armorTypeItem.ItemTypeId)
            {
                List<ArmorSO> armorsSO = armors.ConvertAll(x => (ArmorSO)x).FindAll(j => j.ArmorType.WeareableTypeId == item.ItemWeareableTypeId);

                print($"Item armor {armorsSO[0]}");
            }
            if (item.ItemTypeId == weaponTypeItem.ItemTypeId)
            {
                List<WeaponSO> weaponsSO = armors.ConvertAll(x => (WeaponSO)x).FindAll(j => j.WeaponType.WeareableTypeId == item.ItemWeareableTypeId);

                print($"Item weapon {weaponsSO[0]}");
            }

        }

        private void Load()
        {
            //JsonUtility.FromJsonOverwrite(rosterJson.Value, rosterHeroes);
        }
    }
}