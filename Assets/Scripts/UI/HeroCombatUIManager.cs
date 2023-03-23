using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Skills;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class HeroCombatUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _heroContainer;
        [SerializeField]
        private GameEvent _heroAttackEvent;
        [SerializeField]
        private GameEvent _heroSkillEvent;
        [SerializeField]
        private GameEvent _heroSkipTurnEvent;
        [SerializeField]
        private ActiveHeroSO _activeHero;
        [SerializeField]
        private SkillDatabase _skillDatabase;
        [SerializeField]
        private Image _skillImage;
        [SerializeField]
        private Image _skillCooldownImage;
        [SerializeField]
        private TMP_Text _skillCooldownText;
        [SerializeField]
        private Button _skillButton;

        private Sprite _defaultSkillSprite;
        private Dictionary<Hero, int> _fightersSkillCooldowns;

        public void Attack()
        {
            _heroAttackEvent.Raise();
        }

        public void Skip()
        {
            _heroSkipTurnEvent.Raise();
        }

        private void Skill()
        {
            _heroSkillEvent.Raise();
            Hero hero = _activeHero.ActiveHero;
            SkillSO skillSO = _skillDatabase.GetSkillById(hero.SkillId);
            _fightersSkillCooldowns[hero] = skillSO.SkillTurns + 1;
        }

        private void Start()
        {
            HidePanel();
        }

        private void OnEnable()
        {
            _defaultSkillSprite = _skillImage.sprite;
            _activeHero.OnHeroChanged += ShowPanel;
            _heroAttackEvent.OnRaise += HidePanel;
            _fightersSkillCooldowns = new Dictionary<Hero, int>();
            _skillButton.onClick.AddListener(Skill);
        }

        private void OnDisable()
        {
            _activeHero.OnHeroChanged -= ShowPanel;
            _heroAttackEvent.OnRaise -= HidePanel;
        }

        private void ShowPanel()
        {
            Hero hero = _activeHero.ActiveHero;
            if (hero == null)
            {
                _heroContainer.SetActive(false);
                return;
            }

            _heroContainer.SetActive(true);
            SkillSO skillSO = _skillDatabase.GetSkillById(hero.SkillId);

            if (skillSO == null) 
            {
                _skillButton.enabled = false;
                _skillCooldownText.text = string.Empty;
                _skillImage.sprite = _defaultSkillSprite;
                return; 
            }

            if (!_fightersSkillCooldowns.ContainsKey(hero))
            {
                _fightersSkillCooldowns.Add(hero, skillSO.SkillTurns);
            }
            else 
            {
                _fightersSkillCooldowns[hero]--;
            }

            int turns = _fightersSkillCooldowns[hero];
            _skillImage.sprite = skillSO.SkillSprite;

            _skillButton.enabled = turns <= 0; ;
            _skillCooldownText.text = turns <= 0 ? string.Empty : turns.ToString();
        }

        private void HidePanel()
        {
            _heroContainer.SetActive(false);
        }
    }
}