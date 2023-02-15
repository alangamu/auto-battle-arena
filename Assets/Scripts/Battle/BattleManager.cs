﻿using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using AutoFantasy.Scripts.UI.Mission;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField]
        private HeroCombatRuntimeSet heroCombatSet;
        [SerializeField]
        private HeroCombatRuntimeSet enemyCombatSet;

        [SerializeField]
        private GameEvent heroDefeatEvent;
        [SerializeField]
        private GameEvent heroWinEvent;
        [SerializeField]
        private GameEvent spawnEnemies;
        [SerializeField]
        private GameEvent returnToPositionEvent;

        [SerializeField]
        private DatabaseMission missions;

        [SerializeField]
        private IntVariable missionToLoad;
        [SerializeField]
        private IntVariable currentRound;

        private Mission _mission;

        private void OnEnable()
        {
            currentRound.SetValue(1);
            _mission = missions.GetMission(missionToLoad.Value);
            heroCombatSet.OnHeroCombatEmpty += HeroCombatSet_OnHeroCombatEmpty;
            enemyCombatSet.OnHeroCombatEmpty += EnemyCombatSet_OnHeroCombatEmpty;
        }

        private void OnDisable()
        {
            heroCombatSet.OnHeroCombatEmpty -= HeroCombatSet_OnHeroCombatEmpty;
            enemyCombatSet.OnHeroCombatEmpty -= EnemyCombatSet_OnHeroCombatEmpty;
        }

        private void EnemyCombatSet_OnHeroCombatEmpty()
        {
            if (_mission.Rounds.Count == currentRound.Value)
            {
                heroWinEvent.Raise();
                return;
            }

            currentRound.ApplyChange(1);

            //TODO: remove this when controlling flow in timeline
            Invoke(nameof(SpawnEnemies), 2f);
        }

        private void HeroCombatSet_OnHeroCombatEmpty()
        {
            print($"game over");
            heroDefeatEvent.Raise();
        }

        private void SpawnEnemies()
        {
            returnToPositionEvent.Raise();
            spawnEnemies.Raise();
        }
    }
}