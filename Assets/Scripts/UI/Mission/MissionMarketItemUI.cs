using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI.Mission
{
    public class MissionMarketItemUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text missionNameText;
        [SerializeField]
        private GameObject buyButtonGameObject;
        [SerializeField]
        private Button detailButton;
        [SerializeField]
        private TMP_Text missionCostText;
        [SerializeField]
        private TMP_Text missionDifficultyText;
        [SerializeField]
        private TMP_Text missionRewardText;
        [SerializeField]
        private Image missionTypeIcon;
        [SerializeField]
        private IntVariable gold;
        [SerializeField]
        private GameEvent showMissionsEvent;
        [SerializeField]
        private MissionGameEvent showMissionDetailEvent;
        [SerializeField]
        private BuyButtonMonitor buyButtonMonitor;

        private Mission _mission;
        private Button _buyButton;

        public void Setup(Mission mission)
        {
            _mission = mission;
            missionNameText.text = mission.MissionName;
            missionCostText.text = _mission.SellPrice.ToString();
            missionDifficultyText.text = mission.MissionDifficulty.name;
            missionRewardText.text = $"{mission.GameCurrencyLevel.name[0]} / {mission.LootLevel.name[0]} / {mission.CrownFavorLevel.name[0]}";
            missionTypeIcon.sprite = mission.MissionType.MissionTypeIcon;
            buyButtonMonitor.SetValue(mission.SellPrice);
            
        }

        private void OnEnable()
        {
            detailButton.onClick.AddListener(ShowDetail);
            if (buyButtonGameObject.TryGetComponent(out _buyButton))
            {
                _buyButton.onClick.AddListener(BuyMission);
            }
        }

        private void OnDisable()
        {
            detailButton.onClick.RemoveListener(ShowDetail);
            _buyButton.onClick.RemoveListener(BuyMission);
        }

        private void ShowDetail()
        {
            showMissionDetailEvent.Raise(_mission);
        }

        private void BuyMission()
        {
            gold.ApplyChange(-_mission.SellPrice);
            _mission.Owned = true;
            showMissionsEvent.Raise();
        }
    }
}