using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    public abstract class Database<T> : ScriptableObject
    {
        public List<T> Items;
    }
}