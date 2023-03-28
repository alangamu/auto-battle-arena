using AutoFantasy.Scripts.ScriptableObjects;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class TeamUI : MonoBehaviour
    {
        public Action<int> OnTeamSetup;

        [SerializeField]
        private TeamsSO teams;
        [SerializeField]
        private GameObject selectedUI;
        [SerializeField]
        private GameObject selectedImage;

        public int TeamNumber { get; private set; }

        public void Setup(int teamNumber)
        {
            TeamNumber = teamNumber;
            ActiveTeamChanged();
            OnTeamSetup?.Invoke(TeamNumber);
        }

        //button in the ui
        public void SelectThisTeam()
        {
            teams.SetActiveTeam(TeamNumber);
        }

        private void OnEnable()
        {
            teams.OnSetActiveTeam += ActiveTeamChanged;
        }

        private void OnDisable()
        {
            teams.OnSetActiveTeam -= ActiveTeamChanged;
        }

        private void ActiveTeamChanged()
        {
            SelectThisTeam(TeamNumber == teams.ActiveTeamNumber);
        }

        private void SelectThisTeam(bool isSelected)
        {
            selectedImage.SetActive(isSelected);
            selectedUI.SetActive(isSelected);
        }
    }
}