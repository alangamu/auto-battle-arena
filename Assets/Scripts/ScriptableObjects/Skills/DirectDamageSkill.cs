using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Direct Damage Skill")]
    public class DirectDamageSkill : SkillSO
    {
        [SerializeField]
        protected int _skillPower;

        public override void PerformSkill(List<ICombatController> team, List<ICombatController> enemies)
        {
            base.PerformSkill(team, enemies);
            LeanTween.delayedCall(_attackDelay.Value / 2, () =>
            {
                List<ICombatController> targets = _targetType.GetTargets(team, enemies);
                foreach (var target in targets)
                {
                    target.GettingDamage(_skillPower, true);
                }
            });
        }
    }
}