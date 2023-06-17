using AutoFantasy.Scripts.Heroes;
using System;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IHeroController
    {
        event Action<Hero> OnSetHero;

        void SetHero(Hero hero);
        Hero ThisHero { get; }
    }
}