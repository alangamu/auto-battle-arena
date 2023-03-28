using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.MovementTypes;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    //TODO: try to remove this component
    public class MovementController : MonoBehaviour, IMovementController
    {
        public event Action OnAttackTarget;
        public event Action OnStartRunning;
        public event Action OnSetIdle;

        [SerializeField]
        private MovementTypeSO _movementType;
        [SerializeField]
        private FloatVariable _attackDelayVariable;

        private ICombatController _combatController;

        public void SetMovement(MovementTypeSO movementType)
        {
            _movementType = movementType;
        }

        private void OnEnable()
        {
            if (TryGetComponent(out _combatController))
            {
                _combatController.OnAttackTarget += AttackTarget;
            }
        }

        private void OnDisable()
        {
            if (_combatController != null)
            {
                _combatController.OnAttackTarget -= AttackTarget;
            }
        }

        private async void AttackTarget(Transform target)
        {
            OnAttackTarget?.Invoke();

            _movementType.PerformMovement(transform, target, _attackDelayVariable.Value);

            await Task.Delay((int)(_attackDelayVariable.Value * 500));

            OnSetIdle?.Invoke();
        }
    }
}