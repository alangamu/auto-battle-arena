using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.TargetTypes
{
    [CreateAssetMenu(menuName = "Target Types/Self Target")]
    public class SelfTargetType : TargetTypeSO
    {
        public override List<ICombatController> GetTargets(List<ICombatController> team, List<ICombatController> enemies)
        {
            ICombatController selectedHeroController = team.Find(x => x.IsActive());
            List<ICombatController> targets = new List<ICombatController>
            {
                selectedHeroController
            };
            return targets;
        }
    }
}