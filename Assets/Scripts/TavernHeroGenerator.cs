using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class TavernHeroGenerator : MonoBehaviour
    {
        [SerializeField] 
        private HeroRuntimeSet tavernRoster;

        [SerializeField] 
        private GameEvent addHeroToTavern;


        private void OnEnable()
        {
            addHeroToTavern.OnRaise += AddHeroToTavern;
        }

        private void OnDisable()
        {
            addHeroToTavern.OnRaise -= AddHeroToTavern;
        }

        private void AddHeroToTavern()
        {
            Hero hero = tavernRoster.CreateNewHero();
            tavernRoster.Add(hero);
        }
    }
}