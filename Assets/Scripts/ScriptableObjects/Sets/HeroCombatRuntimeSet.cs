using AutoFantasy.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(menuName = "Sets/HeroCombatRuntimeSet")]
    public class HeroCombatRuntimeSet : RuntimeSet<ICombatController>
    {
        public event Action OnHeroCombatEmpty;
        public event Action<List<Reward>> OnHeroDeath;

        private void OnEnable()
        {
            Items.Clear();    
        }

        public ICombatController GetRandomHero()
        {
            return Items[UnityEngine.Random.Range(0, Items.Count)];
        }

        //public void KillThisHero(ICombatController combatController)
        //{
        //    if (Items.Contains(combatController))
        //    {
        //        Remove(combatController);
        //        OnHeroDeath?.Invoke(combatController.GetRewards());

        //        if (Items.Count == 0)
        //        {
        //            OnHeroCombatEmpty?.Invoke();
        //        }
        //    }
        //}

        //public ICombatController GetClosestEnemy(Vector3 currentPosition)
        //{
        //    if (Items.Count == 0)
        //    {
        //        return null;
        //    }

        //    Transform bestTarget = null;
        //    float closestDistanceSqr = Mathf.Infinity;
        //    int enemyIndex = 0;
        //    for (int i = 0; i < Items.Count; i++)
        //    {
        //        Vector3 directionToTarget = Items[i].GetImpactTransform().position - currentPosition;
        //        float dSqrToTarget = directionToTarget.sqrMagnitude;
        //        if (dSqrToTarget < closestDistanceSqr)
        //        {
        //            closestDistanceSqr = dSqrToTarget;
        //            bestTarget = Items[i].GetImpactTransform();
        //            enemyIndex = i;
        //        }
        //    }

        //    return bestTarget == null ? null : Items[enemyIndex];
        //}
    }
}