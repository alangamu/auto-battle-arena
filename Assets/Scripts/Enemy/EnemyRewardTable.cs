using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EnemyRewardTable : MonoBehaviour, IRewardTable
    {
        [SerializeField]
        private List<Reward> _rewards;

        public List<Reward> GetRewards()
        {
            return _rewards;
        }
    }
}