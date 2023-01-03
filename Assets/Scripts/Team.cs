using AutoFantasy.Scripts.Heroes;
using System;
using System.Collections.Generic;

namespace AutoFantasy.Scripts
{
    [Serializable]
    public class Team
    {
        public Action OnHeroChange;
        public List<string> HeroesIds;

        public void AddHero(Hero hero)
        {
            string heroId = hero.GetHeroId();
            if (HeroesIds.Find(x => x.Equals(heroId)) == null)
            {
                HeroesIds.Add(heroId);
                OnHeroChange?.Invoke();
            }
        }

        public void RemoveHero(Hero hero)
        {
            string heroId = hero.GetHeroId();
            if (HeroesIds.Find(x => x.Equals(heroId)) != null)
            {
                HeroesIds.Remove(heroId);
                OnHeroChange?.Invoke();
            }
        }
    }
}