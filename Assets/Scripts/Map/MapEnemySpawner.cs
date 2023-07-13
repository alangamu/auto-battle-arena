using AutoFantasy.Scripts.Enemy;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapEnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _enemyPrefab;
        [SerializeField]
        private TileRuntimeSet _tiles;
        [SerializeField]
        private GameEvent _spawnMapEnemiesEvent;
        [SerializeField]
        private TileTypeSO _enemyTyleType;

        private void OnEnable()
        {
            _spawnMapEnemiesEvent.OnRaise += SpawnEnemies;
        }

        private void OnDisable()
        {
            _spawnMapEnemiesEvent.OnRaise -= SpawnEnemies;
        }

        private void SpawnEnemies()
        {
            MapEnemyStageSO[] enemyStages = Resources.LoadAll<MapEnemyStageSO>("EnemyStages");

            foreach (var enemyStage in enemyStages)
            {
                ITile tile = _tiles.Items.Find(x => x.GetHex().Q == enemyStage.Q && x.GetHex().R == enemyStage.R);
                GameObject enemy = Instantiate(_enemyPrefab, tile.GetGameObject().transform);

                if (enemy.TryGetComponent(out EnemyController enemyController))
                {
                    GameObject enemyObject = enemyController.Initialize(enemyStage.Enemies[0].EnemyVisualPrefab);

                    if (enemyObject.TryGetComponent(out IAnimationMovementController animationMovementController))
                    {
                        animationMovementController.Animate(enemyStage.Enemies[0].Weapon.WeaponType.IdleAnimationClipName);
                    }
                    if (enemyObject.TryGetComponent(out IWeaponController weaponController))
                    {
                        weaponController.ShowWeapon(enemyStage.Enemies[0].Weapon);
                    }
                    if (enemyObject.TryGetComponent(out BoxCollider collider))
                    {
                        collider.enabled = false;
                    }
                    
                }

                enemy.transform.Rotate(new Vector3(0, 180, 0));
                tile.SetType(_enemyTyleType);
                //tile.GetHex().SetIsWalkable(false);
            }
        }
    }
}