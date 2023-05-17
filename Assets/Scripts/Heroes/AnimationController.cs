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

        [SerializeField]
        private GameEvent _hitTargetEvent;

        [SerializeField]
        private GameEvent _shootEvent;

        private WeaponTypeSO _weaponType;
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
        }

        private void OnDisable()
        {
            heroWinEvent.OnRaise -= SetIdle;
            returnToPositionEvent.OnRaise -= SetIdle;

            if (_weaponController != null)
            {
                _weaponController.OnSetWeapon -= OnSetWeapon;
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

        public void Run()
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

        public void Attack()
        {
            OnAttackTarget();
        }

        public void Idle()
        {
            SetIdle();
        }

        public void Hit()
        {
            _hitTargetEvent.Raise();
        }

        public void Shoot()
        {
            _shootEvent.Raise();
        }
    }
}