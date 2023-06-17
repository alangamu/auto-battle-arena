using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class MouseHeroUISelector : MonoBehaviour, ISelectable
    {
        public bool IsSelected => _isSelected;

        [SerializeField]
        private ActiveHeroSO activeHero;

        private bool _isSelected;

        public void Select(bool option)
        {
            if (TryGetComponent(out IHeroUI hero))
            {
                activeHero.SetHero(hero.Hero);
            }
        }
    }
}