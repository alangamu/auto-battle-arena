using System;
using System.Collections.Generic;

namespace AutoFantasy.Scripts
{
    [Serializable]
    public class CombatStats
    {
        public List<int> Stats;

        public int StatCount(int statId)
        {
            var statCount = Stats.FindAll(x => x == statId);

            return statCount == null ? 0 : statCount.Count;
        }

    }
}