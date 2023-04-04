using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.TargetTypes
{
    [CreateAssetMenu(menuName = "Target Types/Enemy All Targets")]
    public class EnemyAllTargetsType : TargetTypeSO
    {
        public override List<ICombatController> GetTargets(List<ICombatController> team, List<ICombatController> enemies)
        {
            return enemies;
        }
    }
}