using AutoFantasy.Scripts.Interfaces;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(menuName = "Sets/HeroCombatRuntimeSet")]
    public class HeroCombatRuntimeSet : RuntimeSet<ICombatController>
    {
        public event Action OnHeroCombatEmpty;

        public void SelectThisHero(ICombatController combatController)
        {
            DeselectHeroes();
            combatController.SetIsSelected(true);
        }

        public void ActivateThisHero(ICombatController combatController) 
        {
            DeactivateHeroes();
            combatController.SetIsActive(true);
        }

        public ICombatController GetSelectedHero()
        {
            ICombatController combatController = Items.Find(x => x.IsSelected());
            return combatController;
        }

        public ICombatController GetActiveHero()
        {
            ICombatController combatController = Items.Find(x => x.IsActive());
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

        public void DeactivateHeroes()
        {
            foreach (var item in Items)
            {
                item.SetIsActive(false);
            }
        }

        private void OnEnable()
        {
            Items.Clear();    
        }
    }
}