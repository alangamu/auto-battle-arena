using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class SkillDislpay : MonoBehaviour
    {
        [SerializeField]
        private ActiveHeroSO _activeHero;
        [SerializeField]
        private Image _skillImage;
        [SerializeField]
        private HeroRuntimeSet _roster;
        [SerializeField]
        private ItemTypeSO _weaponItemType;
        [SerializeField]
        private SkillDatabase _skillDatabase;

        private Sprite _defaultSkillSprite;

        private void OnEnable()
        {
            _defaultSkillSprite = _skillImage.sprite;
            _activeHero.OnHeroChanged += Setup;
            //TODO: add on weapon changed methos to update this display when equip/unequip weapons
        }

        private void OnDisable()
        {
            _activeHero.OnHeroChanged -= Setup;
        }

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            if (_activeHero == null) return;

            //Hero hero = _roster.GetHeroById(_activeHero.ActiveHero.GetHeroId());
            Hero hero = _activeHero.ActiveHero;

            SkillSO skillSO = _skillDatabase.GetSkillById(hero.SkillId);

            _skillImage.sprite = skillSO != null ? skillSO.SkillSprite : _defaultSkillSprite;
        }
    }
}