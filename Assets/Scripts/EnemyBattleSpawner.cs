using AutoFantasy.Scripts.Enemy;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class EnemyBattleSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> heroesTransforms;

        //[SerializeField]
        //private IntVariable currentRound;

        [SerializeField]
        private GameObject _enemyPrefab;

        [SerializeField]
        private GameEvent spawnEnemies;
        [SerializeField]
        private MapEnemyStageSO _activeEnemyStage;

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
            EnemySO[] enemies = _activeEnemyStage.Enemies;
            for (int i = 0; i < enemies.Length; i++)
            {
                Transform spawnPoint = heroesTransforms[i];
                GameObject enemyGO = Instantiate(enemies[i].EnemyVisualPrefab, spawnPoint.position, spawnPoint.rotation, transform);

                if (enemyGO.TryGetComponent(out IAnimationMovementController animationMovementController))
                {
                    animationMovementController.Animate(enemies[i].Weapon.WeaponType.IdleAnimationClipName);
                }
                if (enemyGO.TryGetComponent(out IWeaponController weaponController))
                {
                    weaponController.ShowWeapon(enemies[i].Weapon);
                }
                //if (enemy.TryGetComponent(out IHealthController healthController))
                //{
                //    healthController.SetDifficulty(mission.MissionDifficulty);
                //}

                if (enemyGO.TryGetComponent(out ICombatController combatController))
                {
                    combatController.SetTeamIndex(i);
                }
            }
        }
    }
}