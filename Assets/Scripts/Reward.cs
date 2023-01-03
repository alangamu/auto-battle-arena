using AutoFantasy.Scripts.ScriptableObjects.Items;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    [Serializable]
    public class Reward 
    {
        [Range(0f, 1f)]
        public float Pct;
        public int MinQty;
        public int MaxQty;
        public ItemSO RewardItem;
        public ItemRaritySO ItemRarity;
    }
}