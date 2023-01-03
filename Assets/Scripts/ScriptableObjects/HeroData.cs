using AutoFantasy.Scripts.ScriptableObjects.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    [Serializable]
    public class HeroData
    {
        public string HeroName;

        [Range(0, 8)]
        public int SkinColorIndex;
        [Range(0, 11)]
        public int HairColorIndex;
        [Range(0, 4)]
        public int EyeColorIndex;
        [Range(0, 3)]
        public int ScarColorIndex;
        [Range(0, 7)]
        public int BodyArtColorIndex;
        [Range(0, 22)]
        public int HeadIndex;
        [Range(0, 37)]
        public int HairIndex;
        public int EyebrowsIndex;
        [Range(0, 18)]
        public int FacialHairIndex;
        [Range(0, 3)]
        public int StubbleColorIndex;

        public bool IsMale;
        public string HeroId;

        //TODO: get out the inventory
        public List<Item> HeroInventory;
    }
}