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
        public event Action<Transform> OnAttackTarget;
        public event Action OnSetIdle;
        public event Action<int, bool> OnGetHit;
        public event Action OnSetReadyToAttack;
        public event Action OnDeath;

        public CombatStats GetCombatStats() => _combatStats;

        public Hero GetHero() => _hero;

        public Transform GetImpactTransform() => _impactPoint;

        //This is for enemies to manipulate the stats from inspector
        [SerializeField]
        private CombatStats _combatStats;

        [SerializeField]
        private Transform _impactPoint;

        [SerializeField]
        private HeroCombatRuntimeSet _heroCombatRuntimeSet;

        private Hero _hero;
        private IRewardTable _rewardTable;
        private IHealthController _healthController;

        public void SetCombatStats(CombatStats newCombatStats)
        {
            _combatStats = newCombatStats;
        }

        public void SetHero(Hero hero)
        {
            _hero = hero;
        }

        public void GettingDamage(int amount, bool isCritical)
        {
            print("getting hit");
            OnGetHit?.Invoke(amount, isCritical);
        }

        public void SetReadyToAttack()
        {
            OnSetReadyToAttack?.Invoke();
        }

        public void PerformAttack(Transform target)
        {
            OnAttackTarget?.Invoke(target);
        }

        public void Hit()
        {
            
        }

        public List<Reward> GetRewards()
        {
            if (TryGetComponent(out _rewardTable))
            {
                return _rewardTable.GetRewards();
            }

            return new List<Reward>();
        }

        private void OnEnable()
        {
            _heroCombatRuntimeSet.Add(this);
            if (TryGetComponent(out _healthController))
            {
                _healthController.OnDeath += KillThisHero;
            }
        }

        private void OnDisable()
        {
            _heroCombatRuntimeSet.Remove(this);
            if (_healthController != null)
            {
                _healthController.OnDeath -= KillThisHero;
            }
        }

        private void KillThisHero()
        {
            print("kill hero");
            _healthController.OnDeath -= KillThisHero;
            _heroCombatRuntimeSet.KillThisHero(this);
            OnDeath?.Invoke();
            //TODO: find a better way
            Invoke(nameof(Death), 1f);
        }

        private void Death()
        {
            Destroy(gameObject);
        }
    }
}