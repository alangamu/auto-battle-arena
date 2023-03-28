﻿using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    [CreateAssetMenu(menuName = "Movements/FrontTargetMovement")]
    public class FrontTargetMovement : MovementTypeSO
    {
        public override void PerformMovement(Transform attacker, Transform target, float movementDuration)
        {
            Vector3 _startingPosition = attacker.position;
            LeanTween.move(attacker.gameObject, target.position * 0.9f, movementDuration / 2).setOnComplete(() =>
            {
                LeanTween.move(attacker.gameObject, _startingPosition, movementDuration / 2);
            });
        }
    }
}