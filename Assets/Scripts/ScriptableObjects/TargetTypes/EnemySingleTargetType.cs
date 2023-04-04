using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.TargetTypes
{
    [CreateAssetMenu(menuName = "Target Types/Enemy Single Target")]
    public class EnemySingleTargetType : TargetTypeSO
    {
        public override List<ICombatController> GetTargets(List<ICombatController> team, List<ICombatController> enemies)
        {
            List<ICombatController> targets = new List<ICombatController>
            {
                enemies.Find(x => x.IsSelected())
            };

            return targets;
        }
    }
}