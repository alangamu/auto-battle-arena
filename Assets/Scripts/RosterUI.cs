using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class RosterUI : MonoBehaviour
    {
        [SerializeField] 
        private HeroRuntimeSet rosterHeroes;
        [SerializeField]
        private HeroRuntimeSet tavernHeroes;

        [SerializeField] 
        private Transform contentTransform;
        [SerializeField] 
        private GameObject rosterHeroUIPrefab;
        [SerializeField]
        private GameEvent refreshUI;
        [SerializeField] 
        protected HeroEvent dropHeroEvent;

        private Hero _heroDroped;

        private void OnEnable()
        {
            refreshUI.OnRaise += FillRosterUI;
            dropHeroEvent.OnRaise += DropHeroEvent_OnRaise;
            rosterHeroes.OnChange += RosterChanged;
            FillRosterUI();
        }

        private void OnDisable()
        {
            refreshUI.OnRaise -= FillRosterUI;
            dropHeroEvent.OnRaise -= DropHeroEvent_OnRaise;
            rosterHeroes.OnChange -= RosterChanged;
        }

        private void RosterChanged()
        {
            tavernHeroes.Remove(_heroDroped);
            FillRosterUI();
        }

        private void DropHeroEvent_OnRaise(Hero hero)
        {
            _heroDroped = hero;
            rosterHeroes.Add(_heroDroped);
        }

        private void AddHeroUI(Hero hero)
        {
            GameObject rosterHeroUI = Instantiate(rosterHeroUIPrefab, contentTransform);

            if (rosterHeroUI.TryGetComponent(out IHeroController heroController))
            {
                heroController.SetHero(hero);
            }
        }

        private void FillRosterUI()
        {
            foreach (Transform item in contentTransform)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in rosterHeroes.Items)
            {
                AddHeroUI(item);
            }
        }
    }
}