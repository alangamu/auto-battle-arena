using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class FrameSelectionUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject selectedFrame;
        [SerializeField]
        private ActiveHeroSO activeHero;

        private Hero _hero;
        private IHeroController _heroController;

        private void HeroChange()
        {
            bool isHeroActive = _hero.GetHeroId().Equals(activeHero.ActiveHero.GetHeroId());
            selectedFrame.SetActive(isHeroActive);
        }

        private void OnEnable()
        {
            activeHero.OnHeroChanged += HeroChange;
            if (TryGetComponent(out _heroController))
            {
                _heroController.OnSetHero += SetHero;
            }
        }

        private void OnDisable()
        {
            activeHero.OnHeroChanged -= HeroChange;
            if (_heroController != null)
            {
                _heroController.OnSetHero -= SetHero;
            }
        }

        private void SetHero(Hero hero)
        {
            _hero = hero;
            HeroChange();
        }
    }
}