using AutoFantasy.Scripts.ScriptableObjects.Items;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    [CreateAssetMenu(menuName = "Movements/Half Target Movement")]
    public class HalfTargetMovement : MovementTypeSO
    {
        [SerializeField]
        private float _distance = 1.5f;
        public override void PerformMovement(Transform attacker, Transform target, float movementDuration, Action action, WeaponTypeSO weaponType)
        {
            Vector3 _startingPosition = attacker.position;
            Vector3 _targetPosition = _startingPosition + Vector3.forward * _distance;
            LeanTween.move(attacker.gameObject, _targetPosition, movementDuration / 4).setOnComplete(() =>
            {
                LeanTween.delayedCall(movementDuration / 2, () =>
                {
                    LeanTween.move(attacker.gameObject, _startingPosition, movementDuration / 4);
                });
            });
        }
    }
}