using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{ 
    public abstract class SkillSO : ScriptableObject
    {
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

        public abstract void PerformSkill();
    }
} 
