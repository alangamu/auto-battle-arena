using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Events
{
    public class BaseDualGameEvent<T, K> : ScriptableObject
    {
        public event Action<T, K> OnRaise;

        public virtual void Raise(T tType, K kType)
        {
            OnRaise?.Invoke(tType, kType);
        }
    }
}