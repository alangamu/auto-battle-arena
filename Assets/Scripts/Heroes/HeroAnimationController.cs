using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.Heroes
{
    public class HeroAnimationController : MonoBehaviour, IAnimationController
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private ItemTypeSO _weaponItemType;
        [SerializeField]
        private ItemDatabase databaseItem;
        [SerializeField]
        private WeaponTypeSO _unarmedType;

        private WeaponTypeSO _weaponType;
        private IHeroController _heroController;
        private Hero _hero;

        public void SetWeaponType(WeaponTypeSO weaponType)
        {
            _weaponType = weaponType;
            SetIdle();
        }

        private void OnEnable()
        {
            if (TryGetComponent(out _heroController))
            {
                _heroController.OnSetHero += SetHero;
            }
        }

        private void OnDisable()
        {
            if (_heroController != null)
            {
                _heroController.OnSetHero -= SetHero;
            }
        }

        private void SetHero(Hero hero)
        {
            _hero = hero;
            ActiveHeroChanged();
        }

        private void ActiveHeroChanged()
        {
            Item item = _hero.HeroInventory.Find(x => x.ItemTypeId == _weaponItemType.ItemTypeId);

            if (item != null)
            {
                ItemSO itemSO = databaseItem.GetItem(item);
                WeaponSO weapon = itemSO as WeaponSO;

                SetWeaponType(weapon.WeaponType);
                return;
            }

            SetWeaponType(_unarmedType); 
        }

        private void SetIdle()
        {
            Animate(_weaponType.IdleAnimationClipName);
        }

        private void Animate(string animationKeyName)
        {
            _animator.speed = 1f;
            _animator.Play(animationKeyName);
        }

        public void Run()
        {
            throw new System.NotImplementedException();
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Idle()
        {
            throw new System.NotImplementedException();
        }

        public void Hit()
        {
            throw new System.NotImplementedException();
        }

        public void Shoot()
        {
            throw new System.NotImplementedException();
        }

        public void GetHit()
        {
            throw new System.NotImplementedException();
        }

        public void Death()
        {
            throw new System.NotImplementedException();
        }

        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }
    }
}