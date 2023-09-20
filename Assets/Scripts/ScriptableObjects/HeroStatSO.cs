using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "HeroStatSO", menuName = "Stats/HeroStatSO", order = 0)]
    public class HeroStatSO : ScriptableObject
    {
        public int StatId;
        public float BaseValue;
        public float MultiplierFactor;
        public Sprite StatIcon;
    }
}