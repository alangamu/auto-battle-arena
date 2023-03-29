using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Direct Damage Skill")]
    public class DirectDamageSkill : SkillSO
    {
        [SerializeField]
        protected int _skillPower;

        public override void PerformSkill(ICombatController attacker, ICombatController target)
        {
            base.PerformSkill(attacker, target);
            LeanTween.delayedCall(_attackDelay.Value / 2, () =>
            {
                target.GettingDamage(_skillPower, true);
            });
        }
    }
}