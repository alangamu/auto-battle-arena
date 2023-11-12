using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Transform _enemyVisualTransform;

        private GameObject _enemyObject;
        private IHealthController _healthController;
        private ICombatController _combatController;
        private IAnimationMovementController _animationController;
        private IWeaponController _weaponController;
        private WeaponTypeSO _weaponType;
        private IEffectController _effectController;

        public GameObject Initialize(GameObject enemyPrefab)
        {
            _enemyObject = Instantiate(enemyPrefab, _enemyVisualTransform);

            if (TryGetComponent(out _healthController))
            {
                _healthController.OnDeath += OnDeath;
            }

            if (TryGetComponent(out _combatController))
            {
                _combatController.OnGetHit += GetHit;
            }

            _enemyObject.TryGetComponent(out _weaponController);
            TryGetComponent(out _animationController);
            _enemyObject.TryGetComponent(out _effectController);

            return _enemyObject;
        }

        public void SetWeaponType(WeaponTypeSO weaponType)
        {
            _weaponType = weaponType;
        }

        private void OnDisable()
        {
            if (_healthController != null)
            {
                _healthController.OnDeath -= OnDeath;
            }

            if (_combatController != null)
            {
                _combatController.OnGetHit -= GetHit;
            }
        }

        async private void GetHit(int amount, bool isCritical)
        {
            if (_weaponController != null && _weaponType != null)
            {
                _animationController?.Animate(_weaponType.GetHitAnimationClipName);
                _effectController?.GetHit();
                await Task.Delay(700);
                _animationController?.Animate(_weaponType.IdleAnimationClipName);
            }
        }

        private void OnDeath()
        {
            if (_weaponController != null && _weaponType != null)
            {
                _animationController?.Animate(_weaponType.DeathAnimationClipName);
            }
        }
    }
}