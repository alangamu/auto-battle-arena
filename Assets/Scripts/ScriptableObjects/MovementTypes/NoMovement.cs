using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    [CreateAssetMenu(menuName = "Movements/NoMovement")]
    public class NoMovement : MovementTypeSO
    {
        public override void PerformMovement(Transform attacker, Transform target, float movementDuration, Action action, WeaponTypeSO weaponType)
        {
            attacker.LookAt(target);

            LeanTween.delayedCall(movementDuration / 4, () =>
            {
                if (attacker.gameObject.TryGetComponent(out IAnimationMovementController animationController))
                {
                    action.Invoke();
                }
            });
            LeanTween.delayedCall(movementDuration / 2, () =>
            {
                if (attacker.gameObject.TryGetComponent(out IAnimationMovementController animationController))
                {
                    animationController.Animate(weaponType.IdleAnimationClipName);
                }
            });

        }
    }
}