using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using AutoFantasy.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts
{
    public class WinStage : MonoBehaviour
    {
        [SerializeField]
        private Button continueButton;
        [SerializeField]
        private WinStageRewardUI rewardWindow;
        [SerializeField]
        private HeroCombatRuntimeSet enemyCombatRuntimeSet;
        [SerializeField]
        private GameEvent winStageGameEvent;
        [SerializeField]
        private GameObject root;
        [SerializeField]
        private MapEnemyStageVariable _activeEnemyStage;

        private void OnEnable()
        {
            winStageGameEvent.OnRaise += WinStageGameEvent_OnRaise;
            continueButton.onClick.AddListener(ShowRewards);
            root.SetActive(false);
        }

        private void OnDisable()
        {
            winStageGameEvent.OnRaise -= WinStageGameEvent_OnRaise;
        }

        private void WinStageGameEvent_OnRaise()
        {
            root.SetActive(true);
            _activeEnemyStage.Value.SetIsDefeated(true);
        }

        private void ShowRewards()
        {
            List<Reward> _rewards = _activeEnemyStage.Value.Rewards;
            rewardWindow.gameObject.SetActive(true);
            rewardWindow.SetRewards(_rewards);
            gameObject.SetActive(false);
        }
    }
}