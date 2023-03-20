using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    [CreateAssetMenu(menuName = "Movements/NoMovement")]
    public class NoMovement : MovementTypeSO
    {
        public override void PerformMovement(Transform attacker, Transform target)
        {
            LeanTween.scale(attacker.gameObject, Vector3.one * 1.5f, _attackDelay.Value / 2).setOnComplete(() =>
            {
                LeanTween.scale(attacker.gameObject, Vector3.one, _attackDelay.Value / 2);
            });
        }
    }
}