using AutoFantasy.Scripts.Interfaces;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.Heroes
{
    public class HeroController : MonoBehaviour, IHeroController
    {
        public event Action<Hero> OnSetHero;

        private Hero _hero;

        public Hero ThisHero => _hero;

        public void SetHero(Hero hero)
        {
            _hero = hero;
            OnSetHero?.Invoke(hero);
        }
    }
}