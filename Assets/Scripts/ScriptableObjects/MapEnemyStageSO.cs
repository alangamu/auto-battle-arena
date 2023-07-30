using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class MapEnemyStageSO : MapElementSO
    {
        public EnemySO[] Enemies => _enemies;
        public List<Reward> Rewards => _rewards;
        public bool IsDefeated => _isDefeated;

        [SerializeField]
        private EnemySO[] _enemies;
        [SerializeField]
        private List<Reward> _rewards;
        [SerializeField]
        private bool _isDefeated;

        public void SetIsDefeated(bool isDefeated) => _isDefeated = isDefeated;
    }
}