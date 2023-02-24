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

        public void SelectThisHero(ICombatController combatController)
        {
            DeselectHeroes();
            combatController.SetIsSelected(true);
        }

        public ICombatController GetSelectedEnemy()
        {
            ICombatController combatController = Items.Find(x => x.IsSelected());
            if (combatController == null)
            {
                return GetRandomHero();
            }
            return combatController;
        }

        public ICombatController GetRandomHero()
        {
            if (Items.Count == 0)
            {
                return null;
            }
            return Items[UnityEngine.Random.Range(0, Items.Count)];
        }

        public void KillThisHero(ICombatController combatController)
        {
            if (Items.Contains(combatController))
            {
                Remove(combatController);
                OnHeroDeath?.Invoke(combatController.GetRewards());

                if (Items.Count == 0)
                {
                    OnHeroCombatEmpty?.Invoke();
                }
            }
        }

        public void DeselectHeroes()
        {
            foreach (var item in Items)
            {
                item.SetIsSelected(false);
            }
        }

        private void OnEnable()
        {
            Items.Clear();    
        }
    }
}