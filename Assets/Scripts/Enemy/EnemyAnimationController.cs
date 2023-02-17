using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EnemyAnimationController : MonoBehaviour, IAnimationController
    {
        [SerializeField]
        private WeaponTypeSO weaponType;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private HeroStatSO attackSpeedStat;
        [SerializeField]
        private GameEvent heroWinEvent;

        private IMovementController _movementController;
        private ICombatController _combatController;
        private float _attackSpeed = 1f;

        private void OnEnable()
        {
            heroWinEvent.OnRaise += SetIdle;

            if (TryGetComponent(out _combatController))
            {
                _attackSpeed = attackSpeedStat.BaseValue + (attackSpeedStat.MultiplierFactor * _combatController.GetCombatStats().StatCount(attackSpeedStat.StatId));
                _combatController.OnDeath += CombatController_OnDeath;
            }

            if (TryGetComponent(out _movementController))
            {
                _movementController.OnStartRunning += StartRunning;
                _movementController.OnAttackTarget += AttackTarget;
                _movementController.OnSetIdle += SetIdle;
            }
        }

        private void CombatController_OnDeath()
        {
            Animate(weaponType.DeathAnimationClipName, 1f);
        }

        private void OnDisable()
        {
            heroWinEvent.OnRaise -= SetIdle;

            if (_movementController != null)
            {
                _movementController.OnStartRunning -= StartRunning;
                _movementController.OnAttackTarget -= AttackTarget;
                _movementController.OnSetIdle -= SetIdle;
            }
            if (_combatController != null)
            {
                _combatController.OnDeath -= CombatController_OnDeath;
            }
        }

        private void AttackTarget()
        {
            int randomIndex = Random.Range(0, weaponType.AttackAnimationClipsNames.Count);
            string randomClipName = weaponType.AttackAnimationClipsNames[randomIndex];

            Animate(randomClipName, _attackSpeed);
        }

        private void StartRunning()
        {
            Animate(weaponType.RunAnimationClipName, 1f);
        }

        private void Start()
        {
            SetIdle();            
        }

        private void SetIdle()
        {
            Animate(weaponType.IdleAnimationClipName, 1f);
        }

        private void Animate(string animationKeyName, float playSpeed)
        {
            animator.speed = playSpeed;
            animator.Play(animationKeyName);
        }

        public void SetWeaponType(WeaponTypeSO weaponType)
        {
            SetIdle();
        }
    }
}