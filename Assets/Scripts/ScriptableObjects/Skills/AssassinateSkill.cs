using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Assassinate")]
    public class AssassinateSkill : SkillSO
    {
        public override void PerformSkill()
        {
            Debug.Log($"PerformSkill {name}");
        }
    }
}