using AutoFantasy.Scripts.Heroes;
using UnityEditor;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(fileName = "HeroRuntimeSet", menuName = "Sets/HeroRuntimeSet", order = 0)]
    public class HeroRuntimeSet : RuntimeSet<Hero>
    {
        public Hero GetHeroById(string heroId)
        {
            return Items.Find(x => x.GetHeroId().Equals(heroId));
        }

        private void OnEnable()
        {
            foreach (var item in Items)
            {
                item.OnInventoryChanged += HeroOnInventoryChanged;
            }
        }

        private void OnDisable()
        {
            foreach (var item in Items)
            {
                item.OnInventoryChanged -= HeroOnInventoryChanged;
            }
        }

        private void HeroOnInventoryChanged()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif            
        }
    }
}