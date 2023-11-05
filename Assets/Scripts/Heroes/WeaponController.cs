using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.Heroes
{
    public class WeaponController : MonoBehaviour, IWeaponController
    {
        public WeaponSO GetWeapon() => _weapon;
        public Transform GetWeaponTransform() => _weapon.WeaponType.IsLeftHanded ? _leftHandBoneTransform :_handBoneTransform;
        
        [SerializeField]
        private Transform _handBoneTransform;
        [SerializeField]
        private Transform _leftHandBoneTransform;

        private WeaponSO _weapon;
        private GameObject _weaponGO;


        public void ShowWeapon(WeaponSO weapon)
        {
            HideWeapon();

            _weapon = weapon;
            if (_weapon.ItemPrefab != null)
            {
                WeaponTypeSO weaponType = _weapon.WeaponType;
                string boneParentName = weaponType.BoneParentName;
                Transform handTransform = weaponType.IsLeftHanded ? _leftHandBoneTransform.Find(boneParentName) : _handBoneTransform.Find(boneParentName);

                _weaponGO = Instantiate(_weapon.ItemPrefab, handTransform);
            }
        }

        public void HideWeapon()
        {
            if ( _weaponGO != null)
            {
                Destroy(_weaponGO);
            }
        }
    }
}