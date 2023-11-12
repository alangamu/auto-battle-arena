using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class AnimationMovementController : MonoBehaviour, IAnimationMovementController
    {
        [SerializeField]
        private Animator _animator;

        public void Animate(string animationClipName)
        {
            if (_animator == null)
            {
                TryGetComponent(out _animator);
            }
            _animator.Play(animationClipName);
        }

        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }
    }
}