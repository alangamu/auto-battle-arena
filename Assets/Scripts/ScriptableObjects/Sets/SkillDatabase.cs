using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Skills;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(menuName = "Database/Skill Database")]
    public class SkillDatabase : Database<SkillSO>
    {
        public SkillSO GetRandomSkillByWeaponType(WeaponTypeSO weaponTypeSO)
        {
            List<SkillSO> weaponTypeSkillList = new List<SkillSO>();

            weaponTypeSkillList = Items.FindAll(x => x.WeaponType == weaponTypeSO);

            int randomIndex = Random.Range(0, weaponTypeSkillList.Count);

            SkillSO skill = weaponTypeSkillList[randomIndex];

            return skill;
        }

        public SkillSO GetSkillById(string skillId)
        {
            SkillSO skillSO = Items.Find(x => x.SkillId.Equals(skillId));
            return skillSO;
        }

        public void PerformSkill(string skillId, List<ICombatController> team, List<ICombatController> targets)
        {
            GetSkillById(skillId).PerformSkill(team, targets);
        }

        private void OnEnable()
        {
            Items = new List<SkillSO>();
            SkillSO[] skillsArray = Resources.LoadAll<SkillSO>("Skills");
            foreach (var item in skillsArray)
            {
                Items.Add(item);
                if (string.IsNullOrEmpty(item.SkillId))
                {
                    Debug.Log("creating skill id");
                    item.CreateID();
                }
            }
            Debug.Log($"Loaded {skillsArray.Length} skills");
        }
    }
}