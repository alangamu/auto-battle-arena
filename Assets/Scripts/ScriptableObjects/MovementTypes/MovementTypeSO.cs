using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.MovementTypes
{
    public abstract class MovementTypeSO : ScriptableObject
    {
        [SerializeField]
        protected FloatVariable _attackDelay;

        public abstract void PerformMovement(Transform attacker, Transform target);
    }
}