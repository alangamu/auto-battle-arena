using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.Battle
{
    public class BattleSelectIndicator : MonoBehaviour, ISelectable
    {
        [SerializeField]
        private GameObject _selectIndicator;

        private ICombatController _combatController;

        public void Select(bool option)
        {
            _selectIndicator.SetActive(option);
        }

        private void OnEnable()
        {
            Select(false);
            if (TryGetComponent(out _combatController))
            {
                _combatController.OnSelectionChanged += Select;
            }
        }

        private void OnDisable()
        {
            if (_combatController != null)
            {
                _combatController.OnSelectionChanged -= Select;
            }
        }
    }
}