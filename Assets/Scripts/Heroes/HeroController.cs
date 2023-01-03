using AutoFantasy.Scripts.Interfaces;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.Heroes
{
    public class HeroController : MonoBehaviour, IHeroController
    {
        public event Action<Hero> OnSetHero;

        public void SetHero(Hero hero)
        {
            OnSetHero?.Invoke(hero);
        }
    }
}