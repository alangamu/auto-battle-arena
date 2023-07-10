using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class MapEnemyStageSO : MapElementSO
    {
        public EnemySO[] Enemies => _enemies;

        [SerializeField]
        private EnemySO[] _enemies;

        public void SetEnemies(EnemySO[] enemies)
        {
            _enemies = enemies;
        }
    }
}