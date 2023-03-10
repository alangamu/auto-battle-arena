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
        private DatabaseItem databaseItem;
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
            Item item = _hero.ThisHeroData.HeroInventory.Find(x => x.ItemTypeId == _weaponItemType.ItemTypeId);

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
    }
}