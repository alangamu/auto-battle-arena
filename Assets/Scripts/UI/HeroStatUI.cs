using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class HeroStatUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text statText;
        [SerializeField]
        private TMP_Text _statNameText;
        [SerializeField]
        private Image statFillImage;
        [SerializeField]
        private Image _statIcon;
        [SerializeField]
        private ActiveHeroSO activeHero;
        [SerializeField]
        private HeroRuntimeSet roster;
        [SerializeField] 
        private IntVariable heroMaxPointsCategory;
        [SerializeField]
        private ItemEvent equipItem;
        [SerializeField]
        private GameEvent refreshUI;
        [SerializeField]
        private HeroStatSO _heroStat;

        private void OnEnable()
        {
            roster.OnChange += RefreshUI;
            equipItem.OnRaise += EquipItem_OnRaise;
            activeHero.OnHeroChanged += RefreshUI;
            refreshUI.OnRaise += RefreshUI;
            Invoke(nameof(RefreshUI), 0.05f);
            _statIcon.sprite = _heroStat.StatIcon;
            _statNameText.text = _heroStat.name;
        }

        private void EquipItem_OnRaise(Item item)
        {
            if (item.ItemStats.Contains(_heroStat.StatId) )
            {
                RefreshUI();
            }
        }

        private void OnDisable()
        {
            equipItem.OnRaise -= EquipItem_OnRaise;
            activeHero.OnHeroChanged -= RefreshUI;
            refreshUI.OnRaise -= RefreshUI;
        }

        private void RefreshUI()
        {
            //TODO: make the fill amount 0 to value fade
            var hero = roster.GetHeroById(activeHero.ActiveHero.GetHeroId());
            var statCount = hero.ThisCombatStats.StatCount(_heroStat.StatId);
            statText.text = statCount.ToString();
            statFillImage.fillAmount = (float)statCount / heroMaxPointsCategory.Value;
        }
    }
}