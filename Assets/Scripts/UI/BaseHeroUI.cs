using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class BaseHeroUI : MonoBehaviour, IHeroUI
    {
        [SerializeField]
        private GameObject parentGameObject;
        [SerializeField] 
        private Camera heroCamera;
        [SerializeField] 
        private RawImage rawImage;
        [SerializeField] 
        private TMP_Text nameText;

        [SerializeField]
        private ActiveHeroSO activeHero;

        private Hero _hero;
        private IHeroController _heroController;

        public Hero Hero => _hero;

        private void OnEnable()
        {
            if (TryGetComponent(out _heroController))
            {
                _heroController.OnSetHero += Setup;
            }
        }

        private void OnDisable()
        {
            if (_heroController != null)
            {
                _heroController.OnSetHero -= Setup;
            }
        }

        private void Setup(Hero hero)
        {
            _hero = hero;
            var tex = new RenderTexture(512, 512, 16);
            heroCamera.targetTexture = tex;
            rawImage.texture = tex;
            if (nameText != null)
            {
                nameText.text = hero.HeroName;
            }
        }

        public ActiveHeroSO GetActiveHero()
        {
            return activeHero;
        }
    }
}