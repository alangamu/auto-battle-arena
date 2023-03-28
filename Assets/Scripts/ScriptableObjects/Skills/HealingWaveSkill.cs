using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Healing Wave")]
    public class HealingWaveSkill : SkillSO
    {
        public override void PerformSkill(ICombatController attacker, ICombatController target)
        {
            _movementType.PerformMovement(attacker.GetGameObject().transform, target.GetGameObject().transform, _attackDelay.Value);
            LeanTween.delayedCall(_attackDelay.Value / 2, () =>
            {
                target.GettingDamage(-_skillPower, true);
            });
        }
    }
}