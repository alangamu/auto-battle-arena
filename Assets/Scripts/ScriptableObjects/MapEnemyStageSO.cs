using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class MapEnemyStageSO : MapElementSO
    {
        public EnemySO[] Enemies => _enemies;
        public List<Reward> Rewards => _rewards;

        [SerializeField]
        private EnemySO[] _enemies;

        [SerializeField]
        private List<Reward> _rewards;

        public void SetEnemies(EnemySO[] enemies)
        {
            _enemies = enemies;
        }

        public void SetRewards(List<Reward> rewards)
        {
            _rewards = rewards;
        }
    }
}