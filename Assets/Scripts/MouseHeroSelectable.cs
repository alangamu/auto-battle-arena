using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class MouseHeroSelectable : MonoBehaviour
    {
        [SerializeField]
        private HeroCombatRuntimeSet _heroCombatRuntimeSet; 

        private ICombatController _combatController;

        private void OnMouseUpAsButton()
        {
            if (_combatController.IsSelected())
            {
                _heroCombatRuntimeSet.DeselectHeroes();
            }
            _heroCombatRuntimeSet.SelectThisHero(_combatController);
        }

        private void OnEnable()
        {
            TryGetComponent(out _combatController);
        }
    }
}