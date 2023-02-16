using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class MovementController : MonoBehaviour, IMovementController
    {
        public event Action OnReachTarget;
        public event Action OnStartRunning;

        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private GameEvent returnToPositionEvent;

        private float _range = 1.0f;
        private ICombatController _combatController;
        private IWeaponController _weaponController;
        private bool _isCombat = false;
        private Transform _target;
        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        private void OnEnable()
        {
            returnToPositionEvent.OnRaise += ReturnToPositionEvent_OnRaise;
            _startingPosition = transform.position;
            _startingRotation = transform.rotation;

            if (TryGetComponent(out _combatController))
            {
                //_combatController.OnAttackTarget += CombatController_OnSetTarget;
            }
            if (TryGetComponent(out _weaponController))
            {
                _weaponController.OnSetWeapon += OnSetWeapon;
            }
        }

        private void OnSetWeapon(WeaponSO weapon)
        {
            _range = weapon.WeaponType.Range;
        }

        private void OnDisable()
        {
            returnToPositionEvent.OnRaise -= ReturnToPositionEvent_OnRaise;

            if (_combatController != null)
            {
                //_combatController.OnAttackTarget -= CombatController_OnSetTarget;
            }
            if (_weaponController != null)
            {
                _weaponController.OnSetWeapon -= OnSetWeapon;
            }
        }

        private void ReturnToPositionEvent_OnRaise()
        {
            transform.position = _startingPosition;
            transform.rotation = _startingRotation;
        }

        private void CombatController_OnSetTarget(Transform target)
        {
            _target = target;
            _isCombat = true;
            if (IsTargetOutOfRange())
            {
                OnStartRunning?.Invoke();
            }
        }

        private void Update()
        {
            if (!_isCombat)
            {
                return;
            }

            if (_target != null)
            {
                transform.LookAt(_target);
            }

            if (IsTargetOutOfRange())
            {
                Vector3 moveDir = (_target.position - transform.position).normalized;
                transform.position = transform.position + moveSpeed * Time.deltaTime * moveDir;
                return;
            }

            _isCombat = false;
            OnReachTarget?.Invoke();
        }

        private bool IsTargetOutOfRange()
        {
            return Vector3.Distance(transform.position, _target.position) > _range;
        }
    }
}