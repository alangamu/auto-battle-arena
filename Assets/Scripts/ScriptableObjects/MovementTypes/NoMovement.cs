using AutoFantasy.Scripts.Interfaces;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    [CreateAssetMenu(menuName = "Movements/NoMovement")]
    public class NoMovement : MovementTypeSO
    {
        public override void PerformMovement(Transform attacker, Transform target, float movementDuration, Action action)
        {
            attacker.LookAt(target);
            IAnimationController animationController;

            LeanTween.delayedCall(movementDuration / 4, () => 
            {
                if (attacker.gameObject.TryGetComponent(out animationController))
                {
                    action.Invoke();
                }
            });

            //LeanTween.scale(attacker.gameObject, Vector3.one * 1.5f, movementDuration / 2).setOnComplete(() =>
            //{
            //    LeanTween.scale(attacker.gameObject, Vector3.one, movementDuration / 2);
            //});
        }
    }
}