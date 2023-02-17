using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class MovementController : MonoBehaviour, IMovementController
    {
        public event Action OnAttackTarget;
        public event Action OnStartRunning;
        public event Action OnSetIdle;

        [SerializeField]
        public FloatVariable _attackDelayVariable;

        private ICombatController _combatController;
        private Vector3 _startingPosition;

        private void OnEnable()
        {
            _startingPosition = transform.position;

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

        private void AttackTarget(Transform target)
        {
            OnAttackTarget?.Invoke();

            Vector3 endPos = new Vector3(target.position.x, 0, target.position.z);

            LeanTween.move(gameObject, endPos, _attackDelayVariable.Value / 2).setOnComplete(() =>
            {
                OnSetIdle?.Invoke();
                LeanTween.move(gameObject, _startingPosition, _attackDelayVariable.Value / 2);
            });
        }
    }
}