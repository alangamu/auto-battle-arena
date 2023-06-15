using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class AnimationMovementController : MonoBehaviour, IAnimationMovementController
    {
        [SerializeField]
        private Animator animator;

        public void Animate(string animationClipName)
        {
            animator.Play(animationClipName);
        }
    }
}