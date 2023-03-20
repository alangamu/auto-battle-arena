using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.MovementTypes;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    public abstract class SkillSO : ScriptableObject
    {
        public WeaponTypeSO WeaponType => _weaponType;
        public Sprite SkillSprite => _skillIcon;
        public string SkillId => _skillId;

        [SerializeField]
        private string _skillName;
        [SerializeField]
        private string _skillDescription;
        [SerializeField]
        private Sprite _skillIcon;
        [SerializeField]
        private int _cooldown;
        [SerializeField]
        private int _splashAmount;
        [SerializeField]
        private WeaponTypeSO _weaponType;
        [SerializeField]
        private MovementTypeSO _movementType;
        [SerializeField]
        private string _skillId;

        public abstract void PerformSkill();

        public void CreateID()
        {
            Guid guid = Guid.NewGuid();
            _skillId = guid.ToString();
        }
    }
} 
