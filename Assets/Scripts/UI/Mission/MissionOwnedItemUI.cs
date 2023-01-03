using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI.Mission
{
    public class MissionOwnedItemUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text missionNameText;
        [SerializeField]
        private Button detailButton;
        [SerializeField]
        private Button goToMissionButton;
        [SerializeField]
        private TMP_Text missionDifficultyText;
        [SerializeField]
        private TMP_Text missionRewardText;
        [SerializeField]
        private MissionGameEvent showMissionDetailEvent;
        [SerializeField]
        private GameEvent showTeamsEvent;
        [SerializeField]
        private IntVariable missionToLoad;
        [SerializeField]
        private IntVariable currentRound;

        private Mission _mission;

        public void Setup(Mission mission)
        {
            _mission = mission;
            missionNameText.text = mission.MissionName;
            missionDifficultyText.text = mission.MissionDifficulty.name;
            missionRewardText.text = $"{mission.GameCurrencyLevel.name[0]} / {mission.LootLevel.name[0]} / {mission.CrownFavorLevel.name[0]}";
        }

        private void OnEnable()
        {
            detailButton.onClick.AddListener(ShowDetail);
            goToMissionButton.onClick.AddListener(GoToMission);
        }

        private void OnDisable()
        {
            detailButton.onClick.RemoveListener(ShowDetail);
            goToMissionButton.onClick.RemoveListener(GoToMission);
        }

        private void ShowDetail()
        {
            showMissionDetailEvent.Raise(_mission);
        }

        private void GoToMission()
        {
            showTeamsEvent.Raise();
            missionToLoad.Value = _mission.MissionId;
            currentRound.SetValue(1);
        }
    }
}