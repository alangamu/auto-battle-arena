using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Double Shot")]
    public class DoubleShotSkill : SkillSO
    {
        public override void PerformSkill()
        {
            Debug.Log($"PerformSkill {name}");
        }
    }
}