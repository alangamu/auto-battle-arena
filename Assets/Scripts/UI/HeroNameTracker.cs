using AutoFantasy.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class HeroNameTracker : MonoBehaviour
    {
        [SerializeField]
        private ActiveHeroSO activeHero;
        [SerializeField]
        private TMP_Text heroNameText;

        private void OnEnable()
        {
            activeHero.OnHeroChanged += RefreshUI;
            RefreshUI();
        }

        private void OnDisable()
        {
            activeHero.OnHeroChanged -= RefreshUI;
        }

        private void RefreshUI()
        {
            heroNameText.text = activeHero.ActiveHero.HeroName;
        }
    }
}