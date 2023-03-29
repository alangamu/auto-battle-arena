using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Direct Damage And Heal")]
    public class DamageAndHealSkill : DirectDamageSkill
    {
        [SerializeField]
        private int _skillHealPower;

        public override void PerformSkill(ICombatController attacker, ICombatController target)
        {
            base.PerformSkill(attacker, target);
            LeanTween.delayedCall(_attackDelay.Value / 2, () =>
            {
                attacker.GettingDamage(-_skillHealPower, true);
            });
        }
    }
}