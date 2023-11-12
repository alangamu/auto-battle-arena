using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    [CreateAssetMenu(menuName = "Movements/FrontTargetMovement")]
    public class FrontTargetMovement : MovementTypeSO
    {
        public override void PerformMovement(Transform attacker, Transform target, float movementDuration, Action action, WeaponTypeSO weaponType)
        {
            Vector3 _startingPosition = attacker.position;

            if (attacker.gameObject.TryGetComponent(out IAnimationMovementController animationController))
            {
                animationController.Animate(weaponType.RunAnimationClipName);

                LeanTween.move(attacker.gameObject, target.position * 0.9f + Vector3.down, movementDuration / 4).setOnComplete(() =>
                {
                    action.Invoke();
                    LeanTween.delayedCall(movementDuration / 2, () =>
                    {
                        animationController.Animate(weaponType.RunAnimationClipName);
                        LeanTween.move(attacker.gameObject, _startingPosition, movementDuration / 4).setOnComplete(() =>
                        {
                            attacker.LookAt(target);
                            animationController.Animate(weaponType.IdleAnimationClipName);
                        });
                    });
                });
            }

            //if (attacker.gameObject.TryGetComponent(out IAnimationController animationController))
            //{
            //    animationController.Run();

            //    LeanTween.move(attacker.gameObject, target.position * 0.9f + Vector3.down, movementDuration / 4).setOnComplete(() =>
            //    {
            //        action.Invoke();
            //        LeanTween.delayedCall(movementDuration / 2, () => 
            //        { 
            //            animationController.Run();
            //            LeanTween.move(attacker.gameObject, _startingPosition, movementDuration / 4).setOnComplete(() =>
            //            {
            //                attacker.LookAt(target);
            //                animationController.Idle();
            //            });
            //        });
            //    });
            //}
        }
    }
}