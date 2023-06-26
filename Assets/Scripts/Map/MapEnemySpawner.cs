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
            EnemySO[] enemies = Resources.LoadAll<EnemySO>("Enemies");

            foreach (var item in enemies)
            {
                GameObject enemy = Instantiate(_enemyPrefab, _tiles.Items.Find(x => x.GetHex().Q == item.Q && x.GetHex().R == item.R).GetGameObject().transform);

                if (enemy.TryGetComponent(out EnemyController enemyController))
                {
                    GameObject enemyObject = enemyController.Initialize(item.EnemyVisualPrefab);

                    if (enemyObject.TryGetComponent(out IAnimationMovementController animationMovementController))
                    {
                        animationMovementController.Animate(item.Weapon.WeaponType.IdleAnimationClipName);
                    }
                    if (enemyObject.TryGetComponent(out IWeaponController weaponController))
                    {
                        weaponController.ShowWeapon(item.Weapon);
                    }
                }

                enemy.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }
}