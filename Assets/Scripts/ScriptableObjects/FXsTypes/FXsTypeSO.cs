using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.FXsTypes
{
    public abstract class FXsTypeSO : ScriptableObject
    {
        public abstract void PerformEffect(Transform attacker, Transform target, float movementDuration);
    }
}