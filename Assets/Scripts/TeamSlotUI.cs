using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class TeamSlotUI : MonoBehaviour
    {
        [SerializeField] 
        private GameObject teamHeroUI;
        [SerializeField] 
        private int slotIndex;
        [SerializeField]
        private TeamsSO teams;
        [SerializeField]
        private TeamUI teamUI;
        [SerializeField]
        private HeroRuntimeSet heroes;

        private Team _team;
        private IHeroController _heroController;

        private void OnEnable()
        {
            teamUI.OnTeamSetup += SetTeamIndex;
            teamHeroUI.SetActive(false);
        }

        private void OnDisable()
        {
            teamUI.OnTeamSetup -= SetTeamIndex;
            if (_team != null)
            {
                _team.OnHeroChange -= RefreshUI;
            }
        }

        private void SetTeamIndex(int teamIndex)
        {
            _team = teams.Teams[teamIndex];
            _team.OnHeroChange += RefreshUI;
            RefreshUI();
        }

        private void RefreshUI()
        {
            if (slotIndex < _team.HeroesIds.Count)
            {
                Hero hero = heroes.Items.Find(x => x.GetHeroId() == _team.HeroesIds[slotIndex]);
                if (hero != null)
                {
                    teamHeroUI.SetActive(true);
                    if (teamHeroUI.TryGetComponent(out _heroController))
                    {
                        _heroController.SetHero(hero);
                    }
                }
            }
        }
    }
}