using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.MovementTypes;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System;
using UnityEditor;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    public abstract class SkillSO : ScriptableObject
    {
        public WeaponTypeSO WeaponType => _weaponType;
        public Sprite SkillSprite => _skillIcon;
        public string SkillId => _skillId;
        public int SkillTurns => _cooldown;

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
        protected MovementTypeSO _movementType;
        [SerializeField]
        private string _skillId;
        [SerializeField]
        protected FloatVariable _attackDelay;

        public virtual void PerformSkill(ICombatController attacker, ICombatController target)
        {
            _movementType.PerformMovement(attacker.GetGameObject().transform, target.GetGameObject().transform, _attackDelay.Value);
        }

        public void CreateID()
        {
            Guid guid = Guid.NewGuid();
            _skillId = guid.ToString();
            EditorUtility.SetDirty(this);
        }
    }
} 
