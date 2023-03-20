using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class EquipItem : MonoBehaviour
    {
        [SerializeField]
        private ItemEvent equipItem;
        [SerializeField]
        private WeareableTypeSO aceptedWeareableType;
        [SerializeField]
        private ItemTypeSO aceptedItemType;
        [SerializeField]
        private ItemTypeSO armorType;
        [SerializeField]
        private ActiveHeroSO activeHero;
        [SerializeField]
        private HeroRuntimeSet roster;
        [SerializeField]
        private ItemDatabase databaseItem;
        [SerializeField]
        private ItemEvent addItemToInventory;
        [SerializeField]
        private GameObject _heroObject;

        private void OnEnable()
        {
            equipItem.OnRaise += EquipItem_OnRaise;
        }

        private void OnDisable()
        {
            equipItem.OnRaise -= EquipItem_OnRaise;
        }

        private void EquipItem_OnRaise(Item item)
        {
            if (item.ItemTypeId == aceptedItemType.ItemTypeId)
            {
                if (item.ItemTypeId == armorType.ItemTypeId)
                {
                    if (item.ItemWeareableTypeId == aceptedWeareableType.WeareableTypeId)
                    {
                        Equip(item, false);
                    }
                    return;
                }

                Equip(item, true);
            }
        }

        private void Equip(Item item, bool isWeapon)
        {
            Hero _hero = roster.GetHeroById(activeHero.ActiveHero.GetHeroId());
            Item oldItem;
            if (isWeapon)
            {
                oldItem = _hero.ThisHeroData.HeroInventory.Find(x => x.ItemTypeId == aceptedItemType.ItemTypeId);
                
                if (_heroObject.TryGetComponent(out IAnimationController animationController))
                {
                    ItemSO itemSO = databaseItem.GetItem(item);
                    WeaponSO weaponSO = itemSO as WeaponSO;

                    animationController.SetWeaponType(weaponSO.WeaponType);
                }

                _hero.SetSkillId(item.SkillId);
            }
            else
            {
                oldItem = _hero.ThisHeroData.HeroInventory.Find(x => x.ItemWeareableTypeId == aceptedWeareableType.WeareableTypeId && x.ItemTypeId == aceptedItemType.ItemTypeId);
            }

            if (oldItem != null)
            {
                _hero.RemoveItem(oldItem);
                addItemToInventory.Raise(oldItem);
            }

            _hero.AddItem(item);
        }
    }
}