using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
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

        private List<Reward> _rewards;

        private void OnEnable()
        {
            _rewards = new List<Reward>();
            winStageGameEvent.OnRaise += WinStageGameEvent_OnRaise;
            enemyCombatRuntimeSet.OnHeroDeath += EnemyCombatRuntimeSet_OnHeroDeath;
            continueButton.onClick.AddListener(ShowRewards);
            root.SetActive(false);
        }

        private void OnDisable()
        {
            winStageGameEvent.OnRaise -= WinStageGameEvent_OnRaise;
            enemyCombatRuntimeSet.OnHeroDeath -= EnemyCombatRuntimeSet_OnHeroDeath;
        }

        private void WinStageGameEvent_OnRaise()
        {
            root.SetActive(true);
        }

        private void EnemyCombatRuntimeSet_OnHeroDeath(List<Reward> rewards)
        {
            foreach (var reward in rewards)
            {
                _rewards.Add(reward);
            }
        }

        private void ShowRewards()
        {
            rewardWindow.gameObject.SetActive(true);
            rewardWindow.SetRewards(_rewards);
            gameObject.SetActive(false);
        }
    }
}