using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.Heroes
{
    public class AnimationController : MonoBehaviour, IAnimationController
    {
        [SerializeField]
        private GameEvent heroWinEvent;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private HeroStatSO attackSpeedStat;
        [SerializeField]
        private GameEvent returnToPositionEvent;

        private WeaponTypeSO _weaponType;
        private IMovementController _movementController;
        private IWeaponController _weaponController;
        private IHealthController _healthController;

        public void SetWeaponType(WeaponTypeSO weaponType)
        {
            _weaponType = weaponType;
            SetIdle();
        }

        private void OnEnable()
        {
            returnToPositionEvent.OnRaise += SetIdle;
            heroWinEvent.OnRaise += SetIdle;

            if (TryGetComponent(out _weaponController))
            {
                _weaponController.OnSetWeapon += OnSetWeapon;
            }
            if (TryGetComponent(out _healthController))
            {
                _healthController.OnDeath += OnDeath;
            }
            if (TryGetComponent(out _movementController))
            {
                _movementController.OnStartRunning += OnStartRunning;
                _movementController.OnAttackTarget += OnAttackTarget;
                _movementController.OnSetIdle += SetIdle;
            }
        }

        private void OnDisable()
        {
            heroWinEvent.OnRaise -= SetIdle;
            returnToPositionEvent.OnRaise -= SetIdle;

            if (_weaponController != null)
            {
                _weaponController.OnSetWeapon -= OnSetWeapon;
            }
            if (_movementController != null)
            {
                _movementController.OnStartRunning -= OnStartRunning;
                _movementController.OnAttackTarget -= OnAttackTarget;
                _movementController.OnSetIdle -= SetIdle;
            }
            if (_healthController != null)
            {
                _healthController.OnDeath -= OnDeath;
            }
        }

        private void OnSetWeapon(WeaponSO weaponSO)
        {
            SetWeaponType(weaponSO.WeaponType);
        }

        private void OnAttackTarget()
        {
            int randomIndex = Random.Range(0, _weaponType.AttackAnimationClipsNames.Count);
            string randomClipName = _weaponType.AttackAnimationClipsNames[randomIndex];

            Animate(randomClipName);
        }

        private void OnDeath()
        {
            Animate(_weaponType.DeathAnimationClipName);
        }

        private void OnStartRunning()
        {
            Animate(_weaponType.RunAnimationClipName);
        }

        private void SetIdle()
        {
            Animate(_weaponType.IdleAnimationClipName);
        }

        private void Animate(string animationKeyName)
        {
            animator.Play(animationKeyName);
        }
    }
}