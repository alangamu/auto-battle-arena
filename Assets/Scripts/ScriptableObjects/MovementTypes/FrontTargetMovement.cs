using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    [CreateAssetMenu(menuName = "Movements/FrontTargetMovement")]
    public class FrontTargetMovement : MovementTypeSO
    {
        public override void PerformMovement(Transform attacker, Transform target)
        {
            Vector3 _startingPosition = attacker.position;
            LeanTween.move(attacker.gameObject, target.position, _attackDelay.Value / 2).setOnComplete(() =>
            {
                LeanTween.move(attacker.gameObject, _startingPosition, _attackDelay.Value / 2);
            });
        }
    }
}