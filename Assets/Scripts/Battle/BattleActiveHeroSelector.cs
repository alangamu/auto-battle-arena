using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace AutoFantasy.Scripts
{
    public class BattleActiveHeroSelector : MonoBehaviour
    {
        class HeroAttackSpeedContainer
        {
            public int AttackSpeed;
            public ICombatController CombatController;
            public bool IsPlayer;
        }

        class AttackSpeedComparer : IComparer<HeroAttackSpeedContainer>
        {
            public int Compare(HeroAttackSpeedContainer x, HeroAttackSpeedContainer y)
            {
                return y.AttackSpeed.CompareTo(x.AttackSpeed);
            }
        }

        [SerializeField]
        private HeroStatSO attackPowerStat;
        [SerializeField]
        private HeroStatSO defenseStat;
        [SerializeField]
        private HeroStatSO criticalChanceStat;
        [SerializeField]
        private HeroStatSO criticalPowerStat;

        [SerializeField]
        private GameEvent _startFightEvent;
        [SerializeField]
        private ActiveHeroSO _activeCombatHero;
        [SerializeField]
        private HeroCombatRuntimeSet _heroCombatRuntimeSet;
        [SerializeField]
        private HeroCombatRuntimeSet _enemyCombatRuntimeSet;
        [SerializeField]
        private HeroStatSO _attackSpeedStat;
        [SerializeField]
        private GameEvent _nextAttackBeginEvent;
        [SerializeField]
        private GameEvent _heroAttackEvent;
        [SerializeField]
        private FloatVariable _attackDelayVariable;

        private List<HeroAttackSpeedContainer> _allHeroes;
        private int _currentIndex = 0;
        private HeroAttackSpeedContainer _activeHero;
        private ICombatController _activeEnemy;
        private bool _isCriticalHit = false;
        private bool _isGameRunning = false;

        private void OnEnable()
        {
            _startFightEvent.OnRaise += SetActiveBattleHero;
            _nextAttackBeginEvent.OnRaise += NextHeroReadyToAttack;
            _heroAttackEvent.OnRaise += PerformHeroAttack;
            _heroCombatRuntimeSet.OnRemove += HeroDeath;
            _enemyCombatRuntimeSet.OnRemove += HeroDeath;
            _enemyCombatRuntimeSet.OnHeroCombatEmpty += NextRound;
        }

        private void OnDisable()
        {
            _startFightEvent.OnRaise -= SetActiveBattleHero;
            _nextAttackBeginEvent.OnRaise -= NextHeroReadyToAttack;
            _heroAttackEvent.OnRaise -= PerformHeroAttack;
            _heroCombatRuntimeSet.OnRemove -= HeroDeath;
            _enemyCombatRuntimeSet.OnRemove -= HeroDeath;
            _enemyCombatRuntimeSet.OnHeroCombatEmpty -= NextRound;
        }

        private void NextRound()
        {
            _isGameRunning = false;
            _heroCombatRuntimeSet.DeselectHeroes();
            _activeCombatHero.SetHero(null);
        }

        private void HeroDeath(ICombatController combatController)
        {
            _allHeroes.Remove(_allHeroes.Find(x => x.CombatController == combatController));
        }

        private async void PerformHeroAttack()
        {
            if (_activeHero.IsPlayer)
            {
                _activeEnemy = _enemyCombatRuntimeSet.GetSelectedEnemy();
            }

            Transform target = _activeEnemy.GetImpactTransform();
            _activeHero.CombatController.PerformAttack(target);

            await Task.Delay((int)(_attackDelayVariable.Value * 1000));
            int damagePoints = GetDamagePoints();
            _activeEnemy.GettingDamage(damagePoints, _isCriticalHit);
            NextHeroReadyToAttack();
        }

        private void NextHeroReadyToAttack()
        {
            if (_isGameRunning)
            {
                if (_currentIndex >= _allHeroes.Count) 
                {
                    _currentIndex = 0;
                }

                _activeHero = _allHeroes[_currentIndex];
                _currentIndex++;

                if (!_activeHero.IsPlayer)
                {
                    _activeEnemy = _heroCombatRuntimeSet.GetRandomHero();
                    _heroAttackEvent.Raise();
                }
                else
                {
                    ICombatController combatController = _activeHero.CombatController;

                    _heroCombatRuntimeSet.SelectThisHero(combatController);
                    _activeCombatHero.SetHero(combatController.GetHero());
                }
            }
        }

        private void SetActiveBattleHero()
        {
            _isGameRunning = true;
            _currentIndex = 0;
            _allHeroes = new List<HeroAttackSpeedContainer>();
            foreach (var item in _heroCombatRuntimeSet.Items)
            {
                HeroAttackSpeedContainer heroContainer = new HeroAttackSpeedContainer();
                heroContainer.AttackSpeed = item.GetCombatStats().StatCount(_attackSpeedStat.StatId);
                heroContainer.CombatController = item;
                heroContainer.IsPlayer = true;
                _allHeroes.Add(heroContainer);
            }

            foreach (var item in _enemyCombatRuntimeSet.Items)
            {
                HeroAttackSpeedContainer heroContainer = new HeroAttackSpeedContainer();
                heroContainer.AttackSpeed = item.GetCombatStats().StatCount(_attackSpeedStat.StatId);
                heroContainer.CombatController = item;
                heroContainer.IsPlayer = false;
                _allHeroes.Add(heroContainer);
            }

            _allHeroes.Sort(new AttackSpeedComparer());

            NextHeroReadyToAttack();
        }

        private int GetDamagePoints()
        {
            CombatStats heroCombatStats = _activeHero.CombatController.GetCombatStats();
            int heroDamage = (int)(attackPowerStat.BaseValue + (attackPowerStat.MultiplierFactor * heroCombatStats.StatCount(attackPowerStat.StatId)));
            int targetDefense = (int)(defenseStat.BaseValue + (defenseStat.MultiplierFactor * _activeEnemy.GetCombatStats().StatCount(defenseStat.StatId)));
            float criticalMult = 1.0f;
            int randomCritical = Random.Range(1, 100);
            int criticalPoints = (int)(criticalChanceStat.BaseValue + (heroCombatStats.StatCount(criticalChanceStat.StatId) * criticalChanceStat.MultiplierFactor));

            _isCriticalHit = false;
            if (randomCritical < criticalPoints)
            {
                _isCriticalHit = true;
                criticalMult = criticalPowerStat.BaseValue + (criticalPowerStat.MultiplierFactor * heroCombatStats.StatCount(criticalPowerStat.StatId));
            }

            int totalDamage = (int)(heroDamage * criticalMult) - targetDefense;

            return totalDamage < 0 ? 0 : totalDamage;
        }
    }
}