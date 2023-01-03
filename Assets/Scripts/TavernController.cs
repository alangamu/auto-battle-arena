using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class TavernController : MonoBehaviour
    {
        [SerializeField] 
        private HeroEvent addHeroToRoster;
        [SerializeField] 
        private HeroEvent removeHeroFromTavern;
        [SerializeField] 
        private GameEvent startCountdown;


        private void OnEnable()
        {
            addHeroToRoster.OnRaise += AddHeroToRoster_OnRaise;
            removeHeroFromTavern.OnRaise += RemoveHeroFromTavern_OnRaise;
        }

        private void OnDisable()
        {
            addHeroToRoster.OnRaise -= AddHeroToRoster_OnRaise;
            removeHeroFromTavern.OnRaise -= RemoveHeroFromTavern_OnRaise;
        }

        private void RemoveHeroFromTavern_OnRaise(Hero hero)
        {
            startCountdown.Raise();
        }

        private void AddHeroToRoster_OnRaise(Hero hero)
        {
            removeHeroFromTavern.Raise(hero);
        }
    }
}