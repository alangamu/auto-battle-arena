using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.TargetTypes
{
    [CreateAssetMenu(menuName = "Target Types/Enemy Multiple Target")]
    public class EnemyMultipleTargetsType : TargetTypeSO
    {
        public override List<ICombatController> GetTargets(List<ICombatController> team, List<ICombatController> enemies)
        {
            ICombatController combatController = enemies.Find(x => x.IsSelected());
            int selectedEnemyindex = combatController == null ? Random.Range(0, enemies.Count) : combatController.GetTeamIndex();
            List<ICombatController> targets = new List<ICombatController>();

            //TODO: change the "1" to splash amount
            targets = enemies.FindAll(x => x.GetTeamIndex() >= selectedEnemyindex - 1 && x.GetTeamIndex() <= selectedEnemyindex + 1);

            return targets;
        }
    }
}