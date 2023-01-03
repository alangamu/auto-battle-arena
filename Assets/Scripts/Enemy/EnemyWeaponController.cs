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

        private void Start()
        {
            OnSetWeapon?.Invoke(weapon);
            Instantiate(weapon.ItemPrefab, handBoneTransform.Find(weapon.WeaponType.BoneParentName));
        }
    }
}