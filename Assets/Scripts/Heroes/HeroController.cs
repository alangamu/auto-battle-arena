using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.Heroes
{
    public class HeroController : MonoBehaviour, IHeroController
    {
        public event Action<Hero> OnSetHero;
        public Hero ThisHero => _hero;

        [SerializeField]
        private ItemTypeSO _weaponType;
        [SerializeField]
        private ItemDatabase databaseItem;
        [SerializeField]
        private WeaponTypeSO _unnarmed;

        private Hero _hero;
        private IWeaponController _weaponController;
        private IAnimationMovementController _animationController;

        private void OnEnable()
        {
            TryGetComponent(out _weaponController);
            TryGetComponent(out _animationController);
        }

        private void OnDisable()
        {
            if (_hero != null)
            {
                _hero.OnAddItem -= OnAddItem;
                _hero.OnRemoveItem -= HeroRemoveItem;
            }
        }

        public void SetHero(Hero hero)
        {
            _hero = hero;
            OnSetHero?.Invoke(hero);
            _hero.OnAddItem += OnAddItem;
            _hero.OnRemoveItem += HeroRemoveItem;

            var _heroWeaponItem = _hero.HeroInventory.Find(x => x.ItemTypeId == _weaponType.ItemTypeId);

            if (_heroWeaponItem != null)
            {
                OnAddItem(_heroWeaponItem);
            }
            else
            {
                if (_weaponController != null)
                {
                    _weaponController.HideWeapon();
                    _animationController?.Animate(_unnarmed.IdleAnimationClipName);
                }
            }

        }

        private void OnAddItem(Item item)
        {
            if (item.ItemTypeId == _weaponType.ItemTypeId)
            {
                if (_weaponController != null)
                {
                    WeaponSO _heroWeapon = databaseItem.GetItem(item) as WeaponSO;
                    _weaponController.ShowWeapon(_heroWeapon);
                    
                    _animationController?.Animate(_heroWeapon.WeaponType.IdleAnimationClipName);
                }
            }
        }

        private void HeroRemoveItem(Item item)
        {
            if (item.ItemTypeId == _weaponType.ItemTypeId)
            {
                if (_weaponController != null)
                {
                    _weaponController.HideWeapon();
                    _animationController?.Animate(_unnarmed.IdleAnimationClipName);
                }
            }
        }
    }
}