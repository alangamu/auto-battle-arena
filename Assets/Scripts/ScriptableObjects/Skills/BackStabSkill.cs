using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/BackStab")]
    public class BackStabSkill : SkillSO
    {
        public override void PerformSkill()
        {
            Debug.Log($"PerformSkill {name}");
        }
    }
}