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
        private ICombatController _combatController;
        private float _attackSpeed = 1f;
        private IWeaponController _weaponController;

        public void SetWeaponType(WeaponTypeSO weaponType)
        {
            _weaponType = weaponType;
            SetIdle();
            if (_combatController != null)
            {
                _attackSpeed = attackSpeedStat.BaseValue + (attackSpeedStat.MultiplierFactor * _combatController.GetCombatStats().StatCount(attackSpeedStat.StatId));
            }
        }

        private void OnEnable()
        {
            returnToPositionEvent.OnRaise += SetIdle;
            heroWinEvent.OnRaise += SetIdle;

            if (TryGetComponent(out _weaponController))
            {
                _weaponController.OnSetWeapon += OnSetWeapon;
            }
            if (TryGetComponent(out _combatController))
            {
                _combatController.OnDeath += OnDeath;
                _combatController.OnSetIdle += SetIdle;
            }
            if (TryGetComponent(out _movementController))
            {
                _movementController.OnStartRunning += OnStartRunning;
                _movementController.OnReachTarget += OnReachTarget;
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
                _movementController.OnReachTarget -= OnReachTarget;
            }
            if (_combatController != null)
            {
                _combatController.OnDeath -= OnDeath;
                _combatController.OnSetIdle -= SetIdle;
            }
        }

        private void OnSetWeapon(WeaponSO weaponSO)
        {
            SetWeaponType(weaponSO.WeaponType);
        }

        private void OnReachTarget()
        {
            int randomIndex = Random.Range(0, _weaponType.AttackAnimationClipsNames.Count);
            string randomClipName = _weaponType.AttackAnimationClipsNames[randomIndex];

            Animate(randomClipName, _attackSpeed);
        }

        private void OnDeath()
        {
            Animate(_weaponType.DeathAnimationClipName, 1f);
        }

        private void OnStartRunning()
        {
            Animate(_weaponType.RunAnimationClipName, 1f);
        }

        private void SetIdle()
        {
            Animate(_weaponType.IdleAnimationClipName, 1f);
        }

        private void Animate(string animationKeyName, float playSpeed)
        {
            animator.speed = playSpeed;
            animator.Play(animationKeyName);
        }
    }
}