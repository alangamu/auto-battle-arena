using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Variables
{ 
    [CreateAssetMenu(menuName = "Variables/Int Variable")]
    public class IntVariable : ScriptableObject
    {
        public event Action OnChange;

    #if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
    #endif
        public int Value;

        public void SetValue(int value)
        {
            Value = value;
            OnChange?.Invoke();
        }

        public void ApplyChange(int amount)
        {
            Value += amount;
            OnChange?.Invoke();
        }

        private void OnValidate()
        {
            OnChange?.Invoke();
        }
    }
}