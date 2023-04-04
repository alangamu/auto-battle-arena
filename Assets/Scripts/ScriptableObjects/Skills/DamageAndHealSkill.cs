using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Direct Damage And Heal")]
    public class DamageAndHealSkill : DirectDamageSkill
    {
        [SerializeField]
        private int _skillHealPower;

        public override void PerformSkill(List<ICombatController> team, List<ICombatController> targets)
        {
            base.PerformSkill(team, targets);
            LeanTween.delayedCall(_attackDelay.Value / 2, () =>
            {
                ICombatController selectedHeroController = team.Find(x => x.IsActive());
                selectedHeroController.GettingDamage(-_skillHealPower, true);
            });
        }
    }
}