using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class MouseHeroSelectable : MonoBehaviour, ISelectable
    {
        [SerializeField]
        private ActiveHeroSO activeHero;

        private Hero _hero;
        private IHeroController _heroController;

        public void Select(bool option)
        {
            if (!activeHero.ActiveHero.GetHeroId().Equals(_hero.GetHeroId()))
            {
                activeHero.SetHero(_hero);
            }
        }

        private void OnEnable()
        {
            if (TryGetComponent(out _heroController))
            {
                _heroController.OnSetHero += SetHero;
            }
        }
        private void OnDisable()
        {
            if (_heroController != null)
            {
                _heroController.OnSetHero -= SetHero;
            }
        }

        private void SetHero(Hero hero)
        {
            _hero = hero;
        }
    }
}