using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI.Mission
{
    public class MissionDetailUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text missionNameText;
        [SerializeField]
        private TMP_Text missionCostText;
        [SerializeField]
        private TMP_Text missionTypeText;
        [SerializeField]
        private TMP_Text difficultyText;
        [SerializeField]
        private TMP_Text gameCurrencyText;
        [SerializeField]
        private TMP_Text lootText;
        [SerializeField]
        private TMP_Text crownFavorText;
        [SerializeField]
        private Image missionIcon;
        [SerializeField]
        private GameObject _buyButton;
        [SerializeField]
        private Button _embarkButton;
        [SerializeField]
        private GameObject buyCostGameObject;
        [SerializeField]
        private IntVariable gold;
        [SerializeField]
        private BuyButtonMonitor buyButtonMonitor;
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
            missionIcon.sprite = mission.MissionImage;
            _buyButton.SetActive(!mission.Owned && gold.Value >= mission.SellPrice);
            _embarkButton.gameObject.SetActive(mission.Owned);
            buyButtonMonitor.SetValue(mission.SellPrice);
            buyCostGameObject.SetActive(!mission.Owned);
            missionCostText.text = mission.SellPrice.ToString();
            missionTypeText.text = mission.MissionType.name;
            difficultyText.text = mission.MissionDifficulty.name;
            gameCurrencyText.text = mission.GameCurrencyLevel.name;
            lootText.text = mission.LootLevel.name;
            crownFavorText.text = mission.CrownFavorLevel.name;

            _embarkButton.onClick.AddListener(GoToMission);
        }

        private void GoToMission()
        {
            showTeamsEvent.Raise();
            missionToLoad.Value = _mission.MissionId;
            currentRound.SetValue(1);
        }
    }
}