using AutoFantasy.Scripts.Heroes;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(fileName = "DatabaseName", menuName = "Database/DatabaseName", order = 0)]
    public class DatabaseName : Database<string>
    {
        [SerializeField] 
        private HeroRuntimeSet tavernRuntimeSet;
        [SerializeField] 
        private HeroRuntimeSet rosterRuntimeSet;

        public string GetRandomHeroName()
        {
            List<Hero> heroes = new List<Hero>();

            heroes.AddRange(tavernRuntimeSet.Items);
            heroes.AddRange(rosterRuntimeSet.Items);

            bool isNameInUse = true;
            string heroName = string.Empty;
            do
            {
                heroName = Items[Random.Range(0, Items.Count)];
                isNameInUse = heroes.Find(y => y.HeroName == heroName) != null;
            } while (isNameInUse);

            return heroName;
        }
    }
}