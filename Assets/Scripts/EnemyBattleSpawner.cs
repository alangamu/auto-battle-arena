using AutoFantasy.Scripts.Enemy;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using AutoFantasy.Scripts.UI.Mission;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class EnemyBattleSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> heroesTransforms;

        [SerializeField]
        private DatabaseMission missions;

        [SerializeField]
        private IntVariable missionToLoad;
        [SerializeField]
        private IntVariable currentRound;

        [SerializeField]
        private GameEvent spawnEnemies;

        private void OnEnable()
        {
            spawnEnemies.OnRaise += SpawnEnemies_OnRaise;
        }

        private void OnDisable()
        {
            spawnEnemies.OnRaise -= SpawnEnemies_OnRaise;
        }

        private void SpawnEnemies_OnRaise()
        {
            Mission mission = missions.GetMission(missionToLoad.Value);
            List<EnemyController> enemies = mission.Rounds[currentRound.Value - 1].Enemies;
            for (int i = 0; i < enemies.Count; i++)
            {
                Transform spawnPoint = heroesTransforms[i];
                var enemy = Instantiate(enemies[i].gameObject, spawnPoint.position, spawnPoint.rotation, transform);

                if (enemy.TryGetComponent(out IHealthController healthController))
                {
                    healthController.SetDifficulty(mission.MissionDifficulty);
                }
            }
        }
    }
}