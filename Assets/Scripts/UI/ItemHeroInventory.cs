using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class ItemHeroInventory : MonoBehaviour, IItemUI
    {
        public bool IsInventory { get; set; }

        [SerializeField]
        private GameObject itemUIPrefab;
        [SerializeField]
        private ActiveHeroSO activeHero;
        [SerializeField]
        private HeroRuntimeSet roster;
        [SerializeField]
        private ItemTypeSO aceptedItemType;
        [SerializeField]
        private WeareableTypeSO aceptedWeareableType;
        [SerializeField]
        private ItemTypeSO armorType;
        [SerializeField]
        private ItemEvent equipItem;

        private Item _item;
        private GameObject _itemUI;

        public Item ItemRef => _item;

        private void Awake()
        {
            Clear();
        }

        private void OnEnable()
        {
            equipItem.OnRaise += EquipItem_OnRaise;
            activeHero.OnHeroChanged += Setup;
            Invoke(nameof(Setup), 0.05f);
        }

        private void OnDisable()
        {
            equipItem.OnRaise -= EquipItem_OnRaise;
            activeHero.OnHeroChanged -= Setup;
        }

        private void EquipItem_OnRaise(Item item)
        {
            if (item.ItemTypeId == aceptedItemType.ItemTypeId)
            {
                if (item.ItemTypeId == armorType.ItemTypeId)
                {
                    if (item.ItemWeareableTypeId == aceptedWeareableType.WeareableTypeId)
                    {
                        Setup(item);
                    }
                    return;
                }

                Setup(item);
            }
        }

        public void Clear()
        {
            _item = null;
            Destroy(_itemUI);
        }

        public void Setup(Item item)
        {
            Clear();
            _item = item;
            _itemUI = Instantiate(itemUIPrefab, transform);
            if (_itemUI.TryGetComponent(out IItemUI newItem))
            {
                newItem.Setup(item);
                newItem.IsInventory = false;
            }
        }

        private void Setup()
        {
            Hero hero = roster.GetHeroById(activeHero.ActiveHero.GetHeroId());

            Item item;
            if (aceptedItemType != armorType)
            {
                item = hero.HeroInventory.Find(x => x.ItemTypeId == aceptedItemType.ItemTypeId);
            }
            else
            {
                item = hero.HeroInventory.Find(x => x.ItemWeareableTypeId == aceptedWeareableType.WeareableTypeId && x.ItemTypeId == aceptedItemType.ItemTypeId);
            }

            if (item != null)
            {
                Setup(item);
                return;
            }

            if (_itemUI != null)
            {
                Destroy(_itemUI);
            }
        }
    }
}