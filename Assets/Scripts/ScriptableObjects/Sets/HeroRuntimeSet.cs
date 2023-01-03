using AutoFantasy.Scripts.Heroes;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(fileName = "HeroRuntimeSet", menuName = "Sets/HeroRuntimeSet", order = 0)]
    public class HeroRuntimeSet : RuntimeSet<Hero>
    {
        public Hero GetHeroById(string heroId)
        {
            return Items.Find(x => x.GetHeroId().Equals(heroId));
        }
    }
}