using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.TargetTypes
{
    [CreateAssetMenu(menuName = "Target Types/Team All Targets")]
    public class TeamAllTargetsType : TargetTypeSO
    {
        public override List<ICombatController> GetTargets(List<ICombatController> team, List<ICombatController> enemies)
        {
            return team;
        }
    }
}