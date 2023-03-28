using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    [CreateAssetMenu(menuName = "Movements/NoMovement")]
    public class NoMovement : MovementTypeSO
    {
        public override void PerformMovement(Transform attacker, Transform target, float movementDuration)
        {
            LeanTween.scale(attacker.gameObject, Vector3.one * 1.5f, movementDuration / 2).setOnComplete(() =>
            {
                LeanTween.scale(attacker.gameObject, Vector3.one, movementDuration / 2);
            });
        }
    }
}