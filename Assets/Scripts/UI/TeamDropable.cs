using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoFantasy.Scripts.UI
{
    public class TeamDropable : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private int slotIndex;
        [SerializeField]
        private TeamsSO teams;
        [SerializeField]
        private TeamUI teamUI;

        private int _teamIndex;

        private void OnEnable()
        {
            teamUI.OnTeamSetup += SetTeamIndex;
        }

        private void OnDisable()
        {
            teamUI.OnTeamSetup -= SetTeamIndex;
        }


        private void SetTeamIndex(int teamIndex)
        {
            _teamIndex = teamIndex;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                IHeroUI heroUI = eventData.pointerDrag.GetComponent<IHeroUI>();

                if (heroUI != null)
                {

                    teams.Teams[_teamIndex].AddHero(heroUI.Hero);
                }
            }
        }
    }
}