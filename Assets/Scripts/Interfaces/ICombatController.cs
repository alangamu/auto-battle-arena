using AutoFantasy.Scripts.Heroes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface ICombatController 
    {
        event Action<Transform> OnAttackTarget;
        event Action OnSetReadyToAttack;
        event Action OnDeath;
        event Action<int, bool> OnGetHit;
        CombatStats GetCombatStats();
        void SetCombatStats(CombatStats combatStats);
        void GettingDamage(int amount, bool isCritical);
        List<Reward> GetRewards();
        Transform GetImpactTransform();
        Hero GetHero();
        void SetHero(Hero hero);
        void PerformAttack(Transform target);
        void SetReadyToAttack();
        void Hit();
    }
}