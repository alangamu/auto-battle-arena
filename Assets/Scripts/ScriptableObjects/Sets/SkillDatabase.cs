using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Skills;
using System.Collections.Generic;
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

        private void OnEnable()
        {
            Items = new List<SkillSO>();
            SkillSO[] skillsArray = Resources.LoadAll<SkillSO>("Skills");
            foreach (var item in skillsArray)
            {
                Items.Add(item);
                if (string.IsNullOrEmpty(item.SkillId))
                {
                    item.CreateID();
                }
            }
            Debug.Log($"Loaded {skillsArray.Length} skills");
        }

        public SkillSO GetSkillById(string skillId)
        {
            SkillSO skillSO = Items.Find(x => x.SkillId.Equals(skillId));
            return skillSO;
        }
    }
}