using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    [CreateAssetMenu(menuName = "Movements/Back Target Movement")]
    public class BackTargetMovement : MovementTypeSO
    {
        public override void PerformMovement(Transform attacker, Transform target, float movementDuration)
        {
            Vector3 _startingPosition = attacker.position;
            Vector3 _targetPosition = _startingPosition + (target.position - _startingPosition) * 1.1f;
            LeanTween.move(attacker.gameObject, _targetPosition, movementDuration / 4).setOnComplete(() =>
            {
                attacker.LookAt(target);
                LeanTween.delayedCall(movementDuration / 2, () => 
                {
                    LeanTween.move(attacker.gameObject, _startingPosition, movementDuration / 4).setOnComplete(() => 
                    {
                        attacker.LookAt(target);
                    }); 
                });
            });
        }
    }
}