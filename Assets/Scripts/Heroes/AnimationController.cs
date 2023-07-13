using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.Heroes
{
    public class AnimationController : MonoBehaviour, IAnimationController
    {
        [SerializeField]
        private Animator _animator;

        private WeaponTypeSO _weaponType;

        public void SetWeaponType(WeaponTypeSO weaponType)
        {
            _weaponType = weaponType;
        }

        public void Run()
        {
            _animator.Play(_weaponType.RunAnimationClipName);
        }

        public void Attack()
        {
            int randomIndex = Random.Range(0, _weaponType.AttackAnimationClipsNames.Count);
            string randomClipName = _weaponType.AttackAnimationClipsNames[randomIndex];

            _animator.Play(randomClipName);
        }

        public void Idle()
        {
            _animator.Play(_weaponType.IdleAnimationClipName);
        }

        public void GetHit()
        {
            throw new System.NotImplementedException();
        }

        public void Death()
        {
            _animator.Play(_weaponType.DeathAnimationClipName);
        }

        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }
    }
}