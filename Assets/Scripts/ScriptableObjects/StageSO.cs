using AutoFantasy.Scripts.Heroes;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Stage/StageSO")]
    public class StageSO : ScriptableObject
    {
        public int StageNumber;
        public List<Hero> EnemyHeroes;
        public bool IsCompleted;

        public List<Reward> Rewards;
    }
}