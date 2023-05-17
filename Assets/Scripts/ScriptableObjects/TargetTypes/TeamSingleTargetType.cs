using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.TargetTypes
{
    [CreateAssetMenu(menuName = "Target Types/Team Single Target")]
    public class TeamSingleTargetType : TargetTypeSO
    {
        public override List<ICombatController> GetTargets(List<ICombatController> team, List<ICombatController> enemies)
        {
            //TODO: null when not selected
            List<ICombatController> targets = new List<ICombatController>
            {
                team.Find(x => x.IsSelected())
            };

            return targets;
        }
    }
}