using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class MouseHeroUISelector : MonoBehaviour, ISelectable
    {
        [SerializeField]
        private ActiveHeroSO activeHero;

        public void Select(bool option)
        {
            if (TryGetComponent(out IHeroUI hero))
            {
                activeHero.SetHero(hero.Hero);
            }
        }
    }
}