using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class WeaponTransformProvider : MonoBehaviour, IWeaponTransformProvider
    {
        [SerializeField]
        private Transform _projectileSpawnerTransform;

        public Transform GetWeaponTransform() => _projectileSpawnerTransform;
    }
}