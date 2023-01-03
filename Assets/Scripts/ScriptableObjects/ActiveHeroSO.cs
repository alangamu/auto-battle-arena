using AutoFantasy.Scripts.Heroes;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ActiveHeroSO", menuName = "ActiveHeroSO")]
    public class ActiveHeroSO : ScriptableObject
    {
        public Action OnHeroChanged;
        public Hero ActiveHero { get; private set; }

        public void SetHero(Hero hero)
        {
            ActiveHero = hero;
            OnHeroChanged?.Invoke();
        }

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
    }
}