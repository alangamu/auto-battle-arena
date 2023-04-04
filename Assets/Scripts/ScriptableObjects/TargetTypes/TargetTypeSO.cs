using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.TargetTypes
{
    public abstract class TargetTypeSO : ScriptableObject
    {
        public abstract List<ICombatController> GetTargets(List<ICombatController> team, List<ICombatController> enemies);
    }
}