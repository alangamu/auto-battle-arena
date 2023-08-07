using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class BattleLogic : MonoBehaviour
    {
        //TODO: refactor this class
        [Serializable]
        class HeroAttackSpeedContainer
        {
            public int AttackSpeed;
            public ICombatController CombatController;
            public bool IsPlayer;
            public string HeroName;
        }

        class AttackSpeedComparer : IComparer<HeroAttackSpeedContainer>
        {
            public int Compare(HeroAttackSpeedContainer x, HeroAttackSpeedContainer y)
            {
                return y.AttackSpeed.CompareTo(x.AttackSpeed);
            }
        }

        [SerializeField]
        private TMP_Text _activeIndex;

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
        private GameEvent _enemyAttackEvent;
        [SerializeField]
        private GameEvent _heroSkillEvent;
        [SerializeField]
        private GameEvent _heroSkipTurnEvent;
        [SerializeField]
        private FloatVariable _attackDelayVariable;
        [SerializeField]
        private FloatVariable _skillDelayVariable;
        [SerializeField]
        private SkillDatabase _skillDatabase;

        private List<HeroAttackSpeedContainer> _allHeroes;
        private int _currentIndex = 0;
        private HeroAttackSpeedContainer _activeHero;
        private ICombatController _activeEnemy;
        private bool _isGameRunning = false;

        private void OnEnable()
        {
            _startFightEvent.OnRaise += SetActiveBattleHero;
            _nextAttackBeginEvent.OnRaise += NextHeroReadyToAttack;
            _heroAttackEvent.OnRaise += PerformHeroAttack;
            _heroCombatRuntimeSet.OnRemove += HeroDeath;
            _enemyCombatRuntimeSet.OnRemove += HeroDeath;
            _enemyCombatRuntimeSet.OnHeroCombatEmpty += NextRound;
            _heroSkillEvent.OnRaise += PerformSkill;
            _heroSkipTurnEvent.OnRaise += SkipTurn;
        }

        private void OnDisable()
        {
            _startFightEvent.OnRaise -= SetActiveBattleHero;
            _nextAttackBeginEvent.OnRaise -= NextHeroReadyToAttack;
            _heroAttackEvent.OnRaise -= PerformHeroAttack;
            _heroCombatRuntimeSet.OnRemove -= HeroDeath;
            _enemyCombatRuntimeSet.OnRemove -= HeroDeath;
            _enemyCombatRuntimeSet.OnHeroCombatEmpty -= NextRound;
            _heroSkillEvent.OnRaise -= PerformSkill;
            _heroSkipTurnEvent.OnRaise -= SkipTurn;
        }

        private void SkipTurn()
        {
            PerformHeroAttack();
        }

        private async void PerformSkill()
        {
            if (_activeHero.IsPlayer)
            {
                _activeEnemy = _enemyCombatRuntimeSet.GetSelectedHero();
                if (_activeEnemy == null)
                {
                    _activeEnemy = _enemyCombatRuntimeSet.GetRandomHero();
                }
                _enemyCombatRuntimeSet.SelectThisHero(_activeEnemy);
            }

            string skillId = _activeHero.CombatController.GetHero().SkillId;

            _skillDatabase.PerformSkill(skillId, _heroCombatRuntimeSet.Items, _enemyCombatRuntimeSet.Items);

            await Task.Delay((int)(_skillDelayVariable.Value * 1000));
            NextHeroReadyToAttack();
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
            await Task.Delay((int)(_attackDelayVariable.Value * 1000));
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
                _activeIndex.text = _activeHero.CombatController.GetTeamIndex().ToString();
                _currentIndex++;

                if (!_activeHero.IsPlayer)
                {
                    _enemyCombatRuntimeSet.ActivateThisHero(_activeHero.CombatController);
                    _activeEnemy = _heroCombatRuntimeSet.GetRandomHero();
                    _heroCombatRuntimeSet.SelectThisHero(_activeEnemy);
                    _enemyAttackEvent.Raise();
                    PerformHeroAttack();
                }
                else
                {
                    ICombatController combatController = _activeHero.CombatController;

                    _heroCombatRuntimeSet.ActivateThisHero(combatController);
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
                heroContainer.HeroName = item.GetGameObject().name;
                _allHeroes.Add(heroContainer);
            }

            foreach (var item in _enemyCombatRuntimeSet.Items)
            {
                HeroAttackSpeedContainer heroContainer = new HeroAttackSpeedContainer();
                heroContainer.AttackSpeed = item.GetCombatStats().StatCount(_attackSpeedStat.StatId);
                heroContainer.CombatController = item;
                heroContainer.IsPlayer = false;
                heroContainer.HeroName = item.GetGameObject().name;
                _allHeroes.Add(heroContainer);
            }

            _allHeroes.Sort(new AttackSpeedComparer());

            NextHeroReadyToAttack();
        }
    }
}