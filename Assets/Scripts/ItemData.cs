using System;
using System.Collections.Generic;

namespace AutoFantasy.Scripts
{
    [Serializable]
    public class ItemData
    {
        public int ItemRarityId;
        public int ItemRefId;

        public List<int> ItemStats;
    }
}