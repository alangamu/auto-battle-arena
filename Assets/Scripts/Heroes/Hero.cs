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
        public event Action<Item> OnAddItem;
        public event Action<Item> OnRemoveItem;
        public List<Item> HeroInventory;

        public int MapPositionQ;
        public int MapPositionR;

        public string SkillId => GetSkillId();

        public Hero(bool isMale, string heroName, List<int> stats)
        {
            ThisHeroData = new HeroData();
            ThisCombatStats = new CombatStats();
            ThisHeroData.IsMale = isMale;
            Randomize(isMale);
            ThisHeroData.HeroName = heroName;
            HeroInventory = new List<Item>();
            ThisCombatStats.Stats = stats;
            ThisHeroData.HeroId = CreateID();
        }

        public string GetHeroId()
        {
            return ThisHeroData.HeroId;
        }

        public void AddItem(Item item)
        {
            HeroInventory.Add(item);
            AddStats(item);
            OnAddItem?.Invoke(item);
        }

        public void RemoveItem(Item item)
        {
            HeroInventory.Remove(item);
            RemoveStats(item);
            OnRemoveItem?.Invoke(item);
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

        private string GetSkillId()
        {
            Item item = HeroInventory.Find(x => !string.IsNullOrEmpty(x.SkillId));
            return item == null ? string.Empty : item.SkillId;
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
