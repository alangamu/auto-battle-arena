using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.FXsTypes;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.MovementTypes;
using AutoFantasy.Scripts.ScriptableObjects.TargetTypes;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    public abstract class SkillSO : ScriptableObject
    {
        public WeaponTypeSO WeaponType => _weaponType;
        public Sprite SkillSprite => _skillIcon;
        public string SkillId => _skillId;
        public int SkillTurns => _cooldown;
        public string SkillName => _skillName;
        public TimelineAsset SkillTimeline => _skillTimeline;

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
        [SerializeField]
        protected TargetTypeSO _targetType;
        [SerializeField]
        private TimelineAsset _skillTimeline;

        public virtual void PerformSkill(List<ICombatController> team, List<ICombatController> enemies)
        {
            ICombatController selectedEnemyController = enemies.Find(x => x.IsSelected());
            ICombatController selectedHeroController = team.Find(x => x.IsActive());
            _movementType.PerformMovement(
                selectedHeroController == null ? null : selectedHeroController.GetGameObject().transform,
                selectedEnemyController == null ? null : selectedEnemyController.GetGameObject().transform,
                _attackDelay.Value, () => { });
        }

        public void CreateID()
        {
            Guid guid = Guid.NewGuid();
            _skillId = guid.ToString();
            EditorUtility.SetDirty(this);
        }
    }
} 
