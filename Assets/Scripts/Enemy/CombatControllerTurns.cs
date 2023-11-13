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
        public event Action<int, bool> OnGetHit;
        public event Action OnDeath;
        public event Action<bool> OnSelectionChanged;
        public event Action<bool> OnActivationChanged;

        public CombatStats GetCombatStats() => _combatStats;
        public Hero GetHero() => _hero;
        public Transform GetImpactTransform() => _impactPoint;
        public GameObject GetGameObject() => gameObject;

        //This is for enemies to manipulate the stats from inspector
        [SerializeField]
        private CombatStats _combatStats;

        [SerializeField]
        private Transform _impactPoint;

        [SerializeField]
        private HeroCombatRuntimeSet _heroCombatRuntimeSet;

        private IRewardTable _rewardTable;
        private IHealthController _healthController;
        private IHeroController _heroController;
        private bool _isSelected;
        private bool _isActive;
        private Hero _hero;
        private int _teamIndex;

        public int GetTeamIndex() => _teamIndex;

        public bool IsActive() => _isActive;

        public void SetIsActive(bool isActive)
        {
            _isActive = isActive;
            OnActivationChanged?.Invoke(_isActive);
        }

        public void SetTeamIndex(int teamIndex) => _teamIndex = teamIndex;

        public void SetCombatStats(CombatStats newCombatStats) => _combatStats = newCombatStats;

        public void GettingDamage(int amount, bool isCritical) => OnGetHit?.Invoke(amount, isCritical);

        public void PerformAttack(Transform target) => OnAttackTarget?.Invoke(target);

        public List<Reward> GetRewards()
        {
            if (TryGetComponent(out _rewardTable))
            {
                return _rewardTable.GetRewards();
            }

            return new List<Reward>();
        }

        public bool IsSelected() => _isSelected;

        public void SetIsSelected(bool isSelected)
        {
            _isSelected = isSelected;
            OnSelectionChanged?.Invoke(isSelected);
        }

        private void OnEnable()
        {
            _isSelected = false;
            _heroCombatRuntimeSet.Add(this);
            if (TryGetComponent(out _healthController))
            {
                _healthController.OnDeath += KillThisHero;
            }
            if (TryGetComponent(out _heroController))
            {
                _heroController.OnSetHero += OnSetHero;
            }
        }

        private void OnSetHero(Hero hero) => _hero = hero;

        private void OnDisable()
        {
            _heroCombatRuntimeSet.Remove(this);
            if (_healthController != null)
            {
                _healthController.OnDeath -= KillThisHero;
            }
            if (_heroController != null)
            {
                _heroController.OnSetHero -= OnSetHero;
            }
        }

        private void KillThisHero()
        {
            _healthController.OnDeath -= KillThisHero;
            _heroCombatRuntimeSet.KillThisHero(this);
            OnDeath?.Invoke();
            //TODO: find a better way
            Invoke(nameof(Death), 2f);
        }

        private void Death()
        {
            Destroy(gameObject);
        }
    }
}