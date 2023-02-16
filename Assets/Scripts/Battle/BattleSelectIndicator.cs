using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;

namespace AutoFantasy.Scripts.Battle
{
    public class BattleSelectIndicator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _selectIndicator;
        [SerializeField]
        private GameEvent _heroAttackEvent;

        private ICombatController _combatController;

        private void OnEnable()
        {
            HideIndicator();
            if (TryGetComponent(out _combatController))
            {
                _combatController.OnSetReadyToAttack += ShowIndicator;
            }
            _heroAttackEvent.OnRaise += HideIndicator;
        }

        private void OnDisable()
        {
            if (_combatController != null)
            {
                _combatController.OnSetReadyToAttack -= ShowIndicator;
            }
            _heroAttackEvent.OnRaise -= HideIndicator;
        }

        private void HideIndicator()
        {
            _selectIndicator.SetActive(false);
        }

        private void ShowIndicator()
        {
            _selectIndicator.SetActive(true);
        }
    }
}