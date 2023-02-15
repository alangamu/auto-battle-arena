using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class CombatControllerTurns : MonoBehaviour, ICombatController
    {
        //public event Action<Transform> OnSetTarget;
        //public event Action OnSetIdle;
        //public event Action OnDeath;
        //public event Action<int, bool> OnGetHit;

        public CombatStats GetCombatStats() => _combatStats;

        //public void GetDamage(int amount, bool isCritical) => OnGetHit?.Invoke(amount, isCritical);

        //public Hero GetHero() => _hero;

        //public Transform GetImpactTransform() => _impactPoint;

        //This is for enemies to manipulate the stats from inspector
        [SerializeField]
        private CombatStats _combatStats;

        public event Action<int, bool> OnGetHit;

        //[SerializeField]
        //private Transform _impactPoint;

        //[SerializeField]
        //private HeroCombatRuntimeSet _heroCombatRuntimeSet;

        //private Hero _hero;
        //private IRewardTable _rewardTable;

        public void SetCombatStats(CombatStats newCombatStats)
        {
            _combatStats = newCombatStats;
        }

        //public List<Reward> GetRewards()
        //{
        //    if (TryGetComponent(out _rewardTable))
        //    {
        //        return _rewardTable.GetRewards();
        //    }

        //    return new List<Reward>();
        //}

        //public void SetHero(Hero hero)
        //{
        //    _hero = hero;
        //}

        //private void OnEnable()
        //{
        //    _heroCombatRuntimeSet.Add(this);
        //}

        //private void OnDisable()
        //{
        //    _heroCombatRuntimeSet.Remove(this);
        //}

        public void SetReadyToAttack()
        {
            throw new NotImplementedException();
        }

        public void GettingDamage(int amount, bool isCritical)
        {
            throw new NotImplementedException();
        }
    }
}