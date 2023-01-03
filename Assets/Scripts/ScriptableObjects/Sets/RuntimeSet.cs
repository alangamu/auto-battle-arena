using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        public Action OnChange;
        public List<T> Items = new List<T>();

        public virtual void Add(T thing)
        {
            if (!Items.Contains(thing))
            {
                Items.Add(thing);
                OnChange?.Invoke();
            }
        }

        public virtual void Remove(T thing)
        {
            if (Items.Contains(thing)) 
            { 
                Items.Remove(thing);
                OnChange?.Invoke();
            }
        }

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
    }
}