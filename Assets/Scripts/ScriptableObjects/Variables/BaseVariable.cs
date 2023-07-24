using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Variables
{
    public class BaseVariable<T> : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public T Value;

        public void SetValue(T value)
        {
            Value = value;
        }
    }
}