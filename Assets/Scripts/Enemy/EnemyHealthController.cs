using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EnemyHealthController : MonoBehaviour, IHealthController
    {
        public event Action OnDeath;
        public event Action<float> OnHealthChange;

        [SerializeField]
        private HeroStatSO healthStat;

        private ICombatController _combatController;

        private int _currentHealth = 1000;
        private int _maxHealth = 1000;

        private void OnEnable()
        {
            if (TryGetComponent(out _combatController))
            {
                _combatController.OnGetHit += CombatControllerOnGetHit;
                _currentHealth = (int)(healthStat.BaseValue + (_combatController.GetCombatStats().StatCount(healthStat.StatId) * healthStat.MultiplierFactor));
            }
        }

        private void CombatControllerOnGetHit(int amount, bool isCritical)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
                OnHealthChange?.Invoke(0f);
                return;
            }

            OnHealthChange?.Invoke((float)_currentHealth / _maxHealth);
        }

        private void OnDisable()
        {
            if (_combatController != null)
            {
                _combatController.OnGetHit -= CombatControllerOnGetHit;
            }
        }

        public void SetDificulty(MissionDifficultySO missionDifficulty)
        {
            float health = _currentHealth * missionDifficulty.DifficultyMultiplier;
            _currentHealth = (int)health;
            _maxHealth = _currentHealth;
        }
    }
}