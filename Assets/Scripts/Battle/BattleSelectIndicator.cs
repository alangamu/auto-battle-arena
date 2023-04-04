using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.Battle
{
    public class BattleSelectIndicator : MonoBehaviour, ISelectable
    {
        [SerializeField]
        private GameObject _selectIndicator;
        [SerializeField]
        private GameObject _activeIndicator;

        private ICombatController _combatController;

        public void Select(bool option)
        {
            _selectIndicator.SetActive(option);
        }

        private void OnEnable()
        {
            Select(false);
            Activation(false);
            if (TryGetComponent(out _combatController))
            {
                _combatController.OnSelectionChanged += Select;
                _combatController.OnActivationChanged += Activation;
            }
        }

        private void Activation(bool isActive)
        {
            _activeIndicator.SetActive(isActive);
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