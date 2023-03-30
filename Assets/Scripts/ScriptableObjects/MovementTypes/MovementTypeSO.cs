using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    public abstract class MovementTypeSO : ScriptableObject
    {
        //TODO: make this base class a float to control the distance to the target
        public abstract void PerformMovement(Transform attacker, Transform target, float movementDuration);
    }
}