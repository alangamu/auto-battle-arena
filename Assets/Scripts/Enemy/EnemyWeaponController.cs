using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EnemyWeaponController : MonoBehaviour, IWeaponController
    {
        [SerializeField]
        private WeaponSO weapon;
        [SerializeField]
        private Transform handBoneTransform;

        public event Action<WeaponSO> OnSetWeapon;

        public WeaponSO GetWeapon() => weapon;

        public Transform GetWeaponTransform() => handBoneTransform;

        public void ShowWeapon(WeaponSO weapon)
        {
            throw new NotImplementedException();
        }

        private void Start()
        {
            OnSetWeapon?.Invoke(weapon);
            Instantiate(weapon.ItemPrefab, handBoneTransform.Find(weapon.WeaponType.BoneParentName));
        }
    }
}