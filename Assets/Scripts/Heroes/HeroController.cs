using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System;
using System.Threading.Tasks;
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
        private IHealthController _healthController;
        private Item _weapon;
        private WeaponSO _heroWeapon;
        private IEffectController _effectController;

        private void OnEnable()
        {
            TryGetComponent(out _weaponController);
            TryGetComponent(out _animationController);
            TryGetComponent(out _healthController);
            TryGetComponent(out _effectController);
        }

        private void OnDisable()
        {
            if (_hero != null)
            {
                _hero.OnAddItem -= SetWeapon;
                _hero.OnRemoveItem -= HeroRemoveItem;
            }
            if (_healthController != null)
            {
                _healthController.OnDeath -= HeroDeath;
                _healthController.OnHealthChange -= GetHit;
            }
        }

        public void SetHero(Hero hero)
        {
            _hero = hero;
            OnSetHero?.Invoke(hero);
            _hero.OnAddItem += SetWeapon;
            _hero.OnRemoveItem += HeroRemoveItem;

            _weapon = _hero.HeroInventory.Find(x => x.ItemTypeId == _weaponType.ItemTypeId);

            if (_weapon != null)
            {
                SetWeapon(_weapon);
            }
            else
            {
                if (_weaponController != null)
                {
                    _weaponController.HideWeapon();
                    _animationController?.Animate(_unnarmed.IdleAnimationClipName);
                }
            }

            if (_healthController != null)
            {
                _healthController.OnDeath += HeroDeath;
                _healthController.OnHealthChange += GetHit;
            }
        }

        private void HeroDeath()
        {
            if (_weaponController != null)
            {
                _healthController.OnHealthChange -= GetHit;
                _animationController?.Animate(_weapon != null ? _heroWeapon.WeaponType.DeathAnimationClipName : _unnarmed.DeathAnimationClipName);
            }
        }

        async private void GetHit(float amount)
        {
            _effectController?.GetHit();
            if (_weaponController != null)
            {
                if (_weapon.ItemRefId != null)
                {
                    _animationController?.Animate(_weaponController.GetWeapon().WeaponType.GetHitAnimationClipName);
                    await Task.Delay(700);
                    _animationController?.Animate(_weaponController.GetWeapon().WeaponType.IdleAnimationClipName);
                    return;
                }

                _animationController?.Animate(_unnarmed.GetHitAnimationClipName);
                await Task.Delay(700);
                _animationController?.Animate(_unnarmed.IdleAnimationClipName);
            }
        }

        private void SetWeapon(Item item)
        {
            if (item.ItemTypeId == _weaponType.ItemTypeId)
            {
                if (_weaponController != null)
                {
                    _heroWeapon = databaseItem.GetItem(item) as WeaponSO;
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