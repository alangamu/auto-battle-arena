using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Healing Wave")]
    public class HealingWaveSkill : SkillSO
    {
        public override void PerformSkill()
        {
            Debug.Log($"PerformSkill {name}");
        }
    }
}