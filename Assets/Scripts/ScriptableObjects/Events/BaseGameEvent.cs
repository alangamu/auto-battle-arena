using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Events
{
    public class BaseGameEvent<T> : ScriptableObject
    {
        public event Action<T> OnRaise;

        public virtual void Raise(T type)
        {
            OnRaise?.Invoke(type);
        }
    }
}
