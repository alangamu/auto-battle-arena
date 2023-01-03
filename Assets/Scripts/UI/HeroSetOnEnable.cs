using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class HeroSetOnEnable : MonoBehaviour
    {
        [SerializeField]
        private HeroRuntimeSet roster;
        [SerializeField]
        private HeroEvent selectRosterHeroEvent;
        [SerializeField]
        private HeroRuntimeSet tavern;
        [SerializeField]
        private HeroEvent selectTavernHeroEvent;

        private void OnEnable()
        {
            selectRosterHeroEvent.Raise(roster.Items[0]);
            selectTavernHeroEvent.Raise(tavern.Items[0]);
        }
    }
}