﻿using AutoFantasy.Scripts.Enemy;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class EnemyBattleSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> heroesTransforms;

        [SerializeField]
        private GameObject _enemyCombatPrefab;

        [SerializeField]
        private GameEvent spawnEnemies;
        [SerializeField]
        private MapEnemyStageVariable _activeEnemyStage;

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
            EnemySO[] enemies = _activeEnemyStage.Value.Enemies;
            for (int i = 0; i < enemies.Length; i++)
            {
                Transform spawnPoint = heroesTransforms[i];
                GameObject enemyGO = Instantiate(_enemyCombatPrefab, spawnPoint.position, spawnPoint.rotation, transform);
            
                if (enemyGO.TryGetComponent(out EnemyController enemyController))
                {
                    GameObject visualEnemy = enemyController.Initialize(enemies[i].EnemyVisualPrefab);
                    enemyController.SetWeaponType(enemies[i].Weapon.WeaponType);
                    
                    if (visualEnemy.TryGetComponent(out IWeaponController weaponController))
                    {
                        weaponController.ShowWeapon(enemies[i].Weapon);
                    }

                    if (enemyGO.TryGetComponent(out IAnimationMovementController animationController))
                    {
                        if (visualEnemy.TryGetComponent(out Animator animator))
                        {
                            animationController.SetAnimator(animator);
                            animationController.Animate(enemies[i].Weapon.WeaponType.IdleAnimationClipName);
                        }
                    }

                    enemyGO.name = visualEnemy.name;
                }

                if (enemyGO.TryGetComponent(out ICombatController combatController))
                {
                    combatController.SetTeamIndex(i);
                }
                if (enemyGO.TryGetComponent(out IHealthController healthController))
                {
                    healthController.SetHealth(enemies[i].MaxHealth);
                }
            }
        }
    }
}