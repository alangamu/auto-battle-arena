using AutoFantasy.Scripts.Interfaces;
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

        public void SetWeaponType(WeaponTypeSO weaponType)
        {
            SetIdle();
        }

        private void StartRunning()
        {
            animator.Play(weaponType.RunAnimationClipName);
        }

        public void Run()
        {
            StartRunning();
        }

        public void Attack()
        {
            int randomIndex = Random.Range(0, weaponType.AttackAnimationClipsNames.Count);
            string randomClipName = weaponType.AttackAnimationClipsNames[randomIndex];

            animator.Play(randomClipName);
        }

        public void Idle()
        {
            SetIdle();
        }

        public void Hit()
        {
            //animator.Play(weaponType.);
        }

        public void Shoot()
        {
            
        }

        public void GetHit()
        {
            //
        }

        public void Death()
        {
            animator.Play(weaponType.DeathAnimationClipName);
        }

        private void SetIdle()
        {
            animator.Play(weaponType.IdleAnimationClipName);
        }
    }
}