using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class StageHeroSpawnerManager : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _stageToLoad;
        [SerializeField]
        private DatabaseStage databaseStage;
        [SerializeField]
        private GameEvent spawnHeroes;
        [SerializeField]
        private GameEvent spawnEnemies;
        [SerializeField]
        private GameEvent startScene;
        [SerializeField]
        private GameEvent startFight;
        [SerializeField]
        private HeroRuntimeSet enemies;

        private void OnEnable()
        {
            startScene.OnRaise += SpawnBattlers;
        }

        private void OnDisable()
        {
            startScene.OnRaise -= SpawnBattlers;
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Start Fight"))
            {
                startFight.Raise();
            }
        }

        private void Start()
        {
            startScene.Raise();
        }

        private void SpawnBattlers()
        {
            spawnEnemies.Raise();
            spawnHeroes.Raise();
        }
    }
}