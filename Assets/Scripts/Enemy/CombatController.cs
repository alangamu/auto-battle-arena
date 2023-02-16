using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class CombatController : MonoBehaviour, ICombatController
    {
        public event Action<Transform> OnAttackTarget;
        public event Action OnSetIdle;
        public event Action<int, bool> OnGetHit;
        public event Action OnDeath;
        public event Action OnSetReadyToAttack;

        public CombatStats GetCombatStats() => combatStats;

        [SerializeField]
        private CombatStats combatStats;
        [SerializeField]
        private HeroCombatRuntimeSet heroCombatRuntimeSet;
        [SerializeField]
        private HeroCombatRuntimeSet enemyCombatRuntimeSet;
        [SerializeField]
        private GameEvent startFight;

        [SerializeField]
        private Transform _impactPoint;

        [SerializeField]
        private HeroStatSO attackPowerStat;
        [SerializeField]
        private HeroStatSO defenseStat;
        [SerializeField]
        private HeroStatSO criticalChanceStat;
        [SerializeField]
        private HeroStatSO criticalPowerStat;

        //TODO: take this prefab from the weaponSO
        [SerializeField]
        private Arrow _projectilePrefab;
        [SerializeField]
        private Transform _projectileSpawnPoint;

        private ICombatController _targetCombatController;
        private IHealthController _healthController;
        private IRewardTable _rewardTable;
        private bool _isCriticalHit;

        /// <summary>
        /// Called from animation event
        /// </summary>
        public void Hit()
        {
            if (_targetCombatController != null)
            {
                int totalDamage = GetDamagePoints();
                _targetCombatController.GettingDamage(totalDamage, _isCriticalHit);
            }
        }

        /// <summary>
        /// Called from animation event
        /// </summary>
        public void Shoot()
        {
            if (_targetCombatController != null)
            {
                Arrow projectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
                //projectile.Launch(_targetCombatController.GetImpactTransform());
                int totalDamage = GetDamagePoints();
                _targetCombatController.GettingDamage(totalDamage, _isCriticalHit);
            }
        }

        public void GettingDamage(int amount, bool isCritical)
        {
            OnGetHit?.Invoke(amount, isCritical);
        }

        public List<Reward> GetRewards()
        {
            if (TryGetComponent(out _rewardTable))
            {
                return _rewardTable.GetRewards();
            }

            return new List<Reward>();
        }

        public void SetCombatStats(CombatStats newCombatStats)
        {
            combatStats = newCombatStats;
        }

        public Transform GetImpactTransform()
        {
            return _impactPoint;
        }

        private void OnEnable()
        {
            heroCombatRuntimeSet.Add(this);
            startFight.OnRaise += SetEnemy;
            if (TryGetComponent(out _healthController))
            {
                _healthController.OnDeath += HealthController_OnDeath;
            }
        }

        private void OnDisable()
        {
            heroCombatRuntimeSet.Remove(this);
            startFight.OnRaise -= SetEnemy;
            if (_healthController != null)
            {
                _healthController.OnDeath -= HealthController_OnDeath;
            }
        }

        private void HealthController_OnDeath()
        {
            _healthController.OnDeath -= HealthController_OnDeath;
            //heroCombatRuntimeSet.KillThisHero(this);
            OnDeath?.Invoke();
            //TODO: find a better way
            Invoke(nameof(Death), 1f);
        }

        private void Death()
        {
            Destroy(gameObject);
        }

        private void SetEnemy()
        {
            //_targetCombatController = enemyCombatRuntimeSet.GetClosestEnemy(transform.position);
            if (_targetCombatController != null)
            {
                //OnSetTarget?.Invoke(_targetCombatController.GetImpactTransform());
                //_targetCombatController.OnDeath += TargetCombatController_OnDeath;
                return;
            }
            OnSetIdle?.Invoke();
        }

        private void TargetCombatController_OnDeath()
        {
            //_targetCombatController.OnDeath -= TargetCombatController_OnDeath;
            SetEnemy();
        }

        private int GetDamagePoints()
        {
            int heroDamage = (int)(attackPowerStat.BaseValue + (attackPowerStat.MultiplierFactor * combatStats.StatCount(attackPowerStat.StatId)));
            int targetDefense = (int)(defenseStat.BaseValue + (defenseStat.MultiplierFactor * _targetCombatController.GetCombatStats().StatCount(defenseStat.StatId)));
            float criticalMult = 1.0f;
            int randomCritical = UnityEngine.Random.Range(1, 100);
            int criticalPoints = (int)(criticalChanceStat.BaseValue + (combatStats.StatCount(criticalChanceStat.StatId) * criticalChanceStat.MultiplierFactor));

            _isCriticalHit = false;
            if (randomCritical < criticalPoints)
            {
                _isCriticalHit = true;
                criticalMult = criticalPowerStat.BaseValue + (criticalPowerStat.MultiplierFactor * combatStats.StatCount(criticalPowerStat.StatId));
            }

            int totalDamage = (int)(heroDamage * criticalMult) - targetDefense;

            return totalDamage < 0 ? 0 : totalDamage;
        }

        public Hero GetHero()
        {
            throw new NotImplementedException();
        }

        public void SetHero(Hero hero)
        {
            throw new NotImplementedException();
        }

        public void SetReadyToAttack()
        {
            throw new NotImplementedException();
        }
    }
}