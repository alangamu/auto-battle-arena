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

        private IHealthController _healthController;

        public void SetWeaponType(WeaponTypeSO weaponType)
        {
            SetIdle();
        }

        public void PlayAnimation(string animationClipName)
        {
            throw new System.NotImplementedException();
        }

        private void OnEnable()
        {
            heroWinEvent.OnRaise += SetIdle;

            if (TryGetComponent(out _healthController))
            {
                _healthController.OnDeath += OnDeath;
            }
        }

        private void OnDeath()
        {
            Animate(weaponType.DeathAnimationClipName, 1f);
        }

        private void OnDisable()
        {
            heroWinEvent.OnRaise -= SetIdle;

            if (_healthController != null)
            {
                _healthController.OnDeath -= OnDeath;
            }
        }

        private void AttackTarget()
        {
            int randomIndex = Random.Range(0, weaponType.AttackAnimationClipsNames.Count);
            string randomClipName = weaponType.AttackAnimationClipsNames[randomIndex];

            Animate(randomClipName, 1f);
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

        public void Run()
        {
            StartRunning();
        }

        public void Attack()
        {
            AttackTarget();
        }

        public void Idle()
        {
            SetIdle();
        }

        public void Hit()
        {
            throw new System.NotImplementedException();
        }

        public void Shoot()
        {
            throw new System.NotImplementedException();
        }
    }
}