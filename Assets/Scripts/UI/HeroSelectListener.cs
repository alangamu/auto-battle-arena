using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class HeroSelectListener : MonoBehaviour
    {
        [SerializeField]
        private ActiveHeroSO activeHero;
        [SerializeField]
        private HeroRuntimeSet roster;

        private IHeroController _heroController;

        private void OnEnable()
        {
            activeHero.OnHeroChanged += SetActiveHero;
            Invoke(nameof(SetActiveHero), 0.01f);
        }

        private void OnDisable()
        {
            activeHero.OnHeroChanged -= SetActiveHero;
        }

        private void SetActiveHero()
        {
            Hero hero = activeHero.ActiveHero;
            if (TryGetComponent(out _heroController))
            {
                _heroController.SetHero(hero);
            }
        }
    }
}