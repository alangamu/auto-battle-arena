using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class HeroSelectListener : MonoBehaviour
    {
        [SerializeField]
        private ActiveHeroSO activeHero;
        [SerializeField]
        private HeroRuntimeSet roster;
        [SerializeField]
        private GameObjectVariable _activeMapHero;

        private IHeroController _heroController;

        private void OnEnable()
        {
            activeHero.OnHeroChanged += SetActiveHero;
            Invoke(nameof(SetActiveMapHero), 0.01f);
        }

        private void OnDisable()
        {
            activeHero.OnHeroChanged -= SetActiveHero;
        }

        private void SetActiveHero()
        {
            SetActiveHero(activeHero.ActiveHero);
        }

        private void SetActiveHero(Hero hero)
        {
            if (TryGetComponent(out _heroController))
            {
                _heroController.SetHero(hero);
            }
        }

        private void SetActiveMapHero()
        {
            if (_activeMapHero.Value.TryGetComponent(out IHeroController heroController))
            {
                activeHero.SetHero(heroController.ThisHero);
            }
        }
    }
}