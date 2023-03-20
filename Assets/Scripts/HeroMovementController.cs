using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class HeroMovementController : MonoBehaviour
    {
        //TODO: maybe get this speed from the hero stats
        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private ItemTypeSO weaponType;
        [SerializeField]
        private GameObjectTransformGameEvent heroSetTarget;
        [SerializeField]
        private GameObjectGameEvent reachTarget;
        [SerializeField]
        private GameObjectGameEvent heroRunEvent;
        [SerializeField]
        private GameObjectGameEvent heroDeathEvent;
        [SerializeField]
        private ItemDatabase databaseItem;
        [SerializeField]
        private GameEvent returnToPositionEvent;

        private bool _isCombat;
        private Transform _target;
        private Vector3 _startingPosition;
        private Quaternion _startingRotation;
        private float _range;
        private IHeroController _heroController;

        private void SetHero(Hero hero)
        {
            var weaponItem = hero.ThisHeroData.HeroInventory.Find(x => x.ItemTypeId == weaponType.ItemTypeId);
            if (weaponItem != null)
            {
                WeaponSO weaponSO = databaseItem.GetItem(weaponItem) as WeaponSO;
                _range = weaponSO.WeaponType.Range;
                return;
            }
            _range = 1.0f;
        }

        private void OnEnable()
        {
            if (TryGetComponent(out _heroController))
            {
                _heroController.OnSetHero += SetHero;
            }

            _isCombat = false;
            heroSetTarget.OnRaise += HeroSetTarget_OnRaise;
            heroDeathEvent.OnRaise += HeroDeathEvent_OnRaise;
            returnToPositionEvent.OnRaise += ReturnToPositionEvent_OnRaise;
            _startingPosition = transform.position;
            _startingRotation = transform.rotation;
        }

        private void OnDisable()
        {
            if (_heroController != null)
            {
                _heroController.OnSetHero -= SetHero;
            }
            heroSetTarget.OnRaise -= HeroSetTarget_OnRaise;
            heroDeathEvent.OnRaise -= HeroDeathEvent_OnRaise;
            returnToPositionEvent.OnRaise -= ReturnToPositionEvent_OnRaise;
        }

        private void ReturnToPositionEvent_OnRaise()
        {
            transform.position = _startingPosition;
            transform.rotation = _startingRotation;
        }

        private void HeroDeathEvent_OnRaise(GameObject heroGameObject)
        {
            if (heroGameObject == gameObject)
            {
                heroSetTarget.OnRaise -= HeroSetTarget_OnRaise;
            }
        }

        private void HeroSetTarget_OnRaise(GameObject hero, Transform targetPosition)
        {
            if (hero == gameObject)
            {
                _target = targetPosition;
                _isCombat = true;
                if (IsTargetOutOfRange())
                {
                    heroRunEvent.Raise(gameObject);
                }
            }
        }

        private void Update()
        {
            if (_target != null)
            {
                transform.LookAt(_target);
            }

            if (!_isCombat)
            {
                return;
            }

            if (IsTargetOutOfRange())
            {
                Vector3 moveDir = (_target.position - transform.position).normalized;
                transform.position = transform.position + moveDir * moveSpeed * Time.deltaTime;
                return;
            }

            reachTarget.Raise(gameObject);
            _isCombat = false;
        }

        private bool IsTargetOutOfRange()
        {
            return Vector3.Distance(transform.position, _target.position) > _range;
        }
    }
}