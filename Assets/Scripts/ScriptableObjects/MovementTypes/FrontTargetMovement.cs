﻿using AutoFantasy.Scripts.Interfaces;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    [CreateAssetMenu(menuName = "Movements/FrontTargetMovement")]
    public class FrontTargetMovement : MovementTypeSO
    {
        public override void PerformMovement(Transform attacker, Transform target, float movementDuration, Action action)
        {
            Vector3 _startingPosition = attacker.position;
            IAnimationController animationController;
            if (attacker.gameObject.TryGetComponent(out animationController))
            {
                animationController.Run();
            }

            LeanTween.move(attacker.gameObject, target.position * 0.9f, movementDuration / 4).setOnComplete(() =>
            {
                action.Invoke();
                LeanTween.delayedCall(movementDuration / 2, () => 
                { 
                    animationController.Run();
                    LeanTween.move(attacker.gameObject, _startingPosition, movementDuration / 4).setOnComplete(() =>
                    {
                        attacker.LookAt(target);
                        animationController.Idle();
                    });
                });
            });
        }
    }
}