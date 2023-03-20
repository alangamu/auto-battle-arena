using AutoFantasy.Scripts.ScriptableObjects.Items;
using System;
using System.Collections.Generic;

namespace AutoFantasy.Scripts.Heroes
{
    [Serializable]
    public class Hero 
    {
        public HeroData ThisHeroData;
        public CombatStats ThisCombatStats;
        public event Action OnInventoryChanged;

        public string SkillId => _skillId;
        public string HeroName => ThisHeroData.HeroName; 
        public int SkinColorIndex => ThisHeroData.SkinColorIndex;
        public int EyeColorIndex => ThisHeroData.EyeColorIndex;
        public int ScarColorIndex => ThisHeroData.ScarColorIndex;
        public int BodyArtColorIndex => ThisHeroData.BodyArtColorIndex;
        public int HairColorIndex => ThisHeroData.HairColorIndex;
        public int HeadIndex => ThisHeroData.HeadIndex;
        public int HairIndex => ThisHeroData.HairIndex;
        public int EyebrowsIndex => ThisHeroData.EyebrowsIndex;
        public bool IsMale => ThisHeroData.IsMale;

        private string _skillId;

        public Hero(bool isMale, string heroName, List<int> stats)
        {
            ThisHeroData = new HeroData();
            ThisCombatStats = new CombatStats();
            ThisHeroData.IsMale = isMale;
            Randomize(isMale);
            ThisHeroData.HeroName = heroName;
            ThisHeroData.HeroInventory = new List<Item>();
            ThisCombatStats.Stats = stats;
            ThisHeroData.HeroId = CreateID();
        }

        public string GetHeroId()
        {
            return ThisHeroData.HeroId;
        }

        public void SetSkillId(string skillId)
        {
            _skillId = skillId;
        }

        public void AddItem(Item item)
        {
            ThisHeroData.HeroInventory.Add(item);
            AddStats(item);
            OnInventoryChanged?.Invoke();
        }

        public void RemoveItem(Item item)
        {
            ThisHeroData.HeroInventory.Remove(item);
            RemoveStats(item);
            OnInventoryChanged?.Invoke();
        }

        public void Randomize(bool isMale)
        {
            ThisHeroData.IsMale = isMale;
            ThisHeroData.EyebrowsIndex = isMale ? UnityEngine.Random.Range(0, 9) : UnityEngine.Random.Range(0, 6);
            ThisHeroData.SkinColorIndex = UnityEngine.Random.Range(0, 8);
            ThisHeroData.HairColorIndex = UnityEngine.Random.Range(0, 11);
            ThisHeroData.EyeColorIndex = UnityEngine.Random.Range(0, 4);
            ThisHeroData.HeadIndex = UnityEngine.Random.Range(0, 22);
            ThisHeroData.HairIndex = UnityEngine.Random.Range(0, 37);
            ThisHeroData.ScarColorIndex = UnityEngine.Random.Range(0, 3);
            ThisHeroData.BodyArtColorIndex = UnityEngine.Random.Range(0, 7);
            ThisHeroData.FacialHairIndex = isMale ? UnityEngine.Random.Range(0, 18) : 0;
            ThisHeroData.StubbleColorIndex = UnityEngine.Random.Range(0, 3);
        }

        private void AddStats(Item item)
        {
            foreach (var stat in item.ItemStats)
            {
                ThisCombatStats.Stats.Add(stat);
            }
        }

        private void RemoveStats(Item item)
        {
            foreach (var stat in item.ItemStats)
            {
                ThisCombatStats.Stats.Remove(stat);
            }
        }

        private string CreateID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}
