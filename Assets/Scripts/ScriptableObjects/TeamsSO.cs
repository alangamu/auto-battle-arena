using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TeamsSO", menuName = "TeamsSO")]
    public class TeamsSO : ScriptableObject
    {
        public Action OnSetActiveTeam;

        public int ActiveTeamNumber;

        public List<Team> Teams;

        public void SetActiveTeam(int teamNumber)
        {
            ActiveTeamNumber = teamNumber;
            OnSetActiveTeam?.Invoke();
        }

        public void SetTeams(List<Team> teams)
        {
            Teams = teams;
        }

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
    }
}