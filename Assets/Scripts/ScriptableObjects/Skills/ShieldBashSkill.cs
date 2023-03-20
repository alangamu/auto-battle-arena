using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Shield Bash")]
    public class ShieldBashSkill : SkillSO
    {
        public override void PerformSkill()
        {
            Debug.Log($"PerformSkill {name}");
        }
    }
}