using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IAnimationMovementController
    {
        void Animate(string animationClipName);
        void SetAnimator(Animator animator);
    }
}