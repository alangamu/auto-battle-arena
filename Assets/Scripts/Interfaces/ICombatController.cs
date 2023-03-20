using AutoFantasy.Scripts.Heroes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface ICombatController 
    {
        event Action<Transform> OnAttackTarget;
        event Action OnDeath;
        event Action<int, bool> OnGetHit;
        event Action<bool> OnSelectionChanged;
        CombatStats GetCombatStats();
        void SetCombatStats(CombatStats combatStats);
        void GettingDamage(int amount, bool isCritical);
        List<Reward> GetRewards();
        Transform GetImpactTransform();
        Hero GetHero();
        void PerformAttack(Transform target);
        void PerformSkill(Transform target);
        void Hit();
        bool IsSelected();
        void SetIsSelected(bool isSelected);
    }
}