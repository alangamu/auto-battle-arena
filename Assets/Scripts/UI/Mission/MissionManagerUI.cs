using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.UI.Mission
{
    public class MissionManagerUI : MonoBehaviour
    {
        [SerializeField]
        private GameEvent showMissionMarketEvent;
        [SerializeField]
        private GameEvent showMissionOwnedEvent;
        [SerializeField]
        private Transform contentTransform;
        [SerializeField]
        private MissionMarketItemUI missionMarketItemPrefab;
        [SerializeField]
        private MissionOwnedItemUI missionOwnedItemPrefab;
        [SerializeField]
        private DatabaseMission databaseMission;
        [SerializeField]
        private MissionGameEvent showMissionDetailEvent;
        [SerializeField]
        private MissionDetailUI missionDetails;

        [SerializeField]
        private List<MissionTabSelectorUI> buttons;

        private void OnEnable()
        {
            showMissionMarketEvent.OnRaise += ShowMissionMarketEvent_OnRaise;
            showMissionOwnedEvent.OnRaise += ShowMissionOwnedEvent_OnRaise;
            showMissionDetailEvent.OnRaise += ShowMissionDetailEvent_OnRaise;
        }

        private void OnDisable()
        {
            showMissionMarketEvent.OnRaise -= ShowMissionMarketEvent_OnRaise;
            showMissionOwnedEvent.OnRaise -= ShowMissionOwnedEvent_OnRaise;
            showMissionDetailEvent.OnRaise -= ShowMissionDetailEvent_OnRaise;
        }

        private void ShowMissionDetailEvent_OnRaise(Mission mission)
        {
            missionDetails.gameObject.SetActive(true);
            missionDetails.Setup(mission);
        }

        private void Start()
        {
            showMissionOwnedEvent.Raise();
        }

        private void ShowMissionOwnedEvent_OnRaise()
        {
            ClearContent(true);
            FillOwnedList();
        }

        private void ShowMissionMarketEvent_OnRaise()
        {
            ClearContent(false);
            FillMarketList();
        }

        private void ClearContent(bool ownedMissions)
        {
            foreach (var button in buttons)
            {
                button.Select(ownedMissions);
            }

            foreach (Transform child in contentTransform)
            {
                Destroy(child.gameObject);
            }
        }

        private void FillMarketList()
        {
            List<Mission> missions = databaseMission.Items.FindAll(x => !x.Owned);

            foreach (var mission in missions)
            {
                MissionMarketItemUI missionMarketItemUIObject = Instantiate(missionMarketItemPrefab, contentTransform);
                missionMarketItemUIObject.Setup(mission);
            }
        }

        private void FillOwnedList()
        {
            List<Mission> missions = databaseMission.Items.FindAll(x => x.Owned);

            foreach (var mission in missions)
            {
                MissionOwnedItemUI missionOwnedItemUIObject = Instantiate(missionOwnedItemPrefab, contentTransform);
                missionOwnedItemUIObject.Setup(mission);
            }
        }
    }
}