using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class HeroWeaponController : MonoBehaviour, IWeaponController
    {
        public event Action<WeaponSO> OnSetWeapon;

        [SerializeField]
        private Transform handBoneTransform;
        [SerializeField]
        private Transform leftHandBoneTransform;
        [SerializeField]
        private GameObject thisHero;
        [SerializeField]
        private ItemTypeSO weaponType;
        [SerializeField]
        private GameEvent refreshUI;
        [SerializeField]
        private ItemDatabase databaseItem;

        private GameObject _weaponGO;
        private Hero _hero;
        private WeaponSO _heroWeapon;
        private IHeroController _heroController;

        public WeaponSO GetWeapon() => _heroWeapon;

        public Transform GetWeaponTransform()
        {
            Transform weaponTransform = handBoneTransform;
            if (_weaponGO.TryGetComponent(out IWeaponTransformProvider weaponTransformProvider) )
            {
                weaponTransform = weaponTransformProvider.GetWeaponTransform();
            }

            return weaponTransform;
        }

        private void OnEnable()
        {
            if (TryGetComponent(out _heroController))
            {
                _heroController.OnSetHero += HeroSetEvent_OnRaise;
            }

            refreshUI.OnRaise += Refresh;
        }

        private void OnDisable()
        {
            if (_heroController != null)
            {
                _heroController.OnSetHero -= HeroSetEvent_OnRaise;
            }
            refreshUI.OnRaise -= Refresh;
        }

        private void Refresh()
        {
            if (_weaponGO != null)
            {
                Destroy(_weaponGO);
            }

            var _heroWeaponItem = _hero.ThisHeroData.HeroInventory.Find(x => x.ItemTypeId == weaponType.ItemTypeId);

            if (_heroWeaponItem != null)
            {
                _heroWeapon = databaseItem.GetItem(_heroWeaponItem) as WeaponSO;
                if (_heroWeapon.ItemPrefab != null)
                {
                    WeaponTypeSO weaponType = _heroWeapon.WeaponType;
                    string boneParentName = weaponType.BoneParentName;
                    Transform handTransform = weaponType.IsLeftHanded ? leftHandBoneTransform.Find(boneParentName) : handBoneTransform.Find(boneParentName);

                    OnSetWeapon?.Invoke(_heroWeapon);
                    _weaponGO = Instantiate(_heroWeapon.ItemPrefab, handTransform);
                }
            }
        }

        private void HeroSetEvent_OnRaise(Hero hero)
        {
            _hero = hero;
            Refresh();
        }
    }
}