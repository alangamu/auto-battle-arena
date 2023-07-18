using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Variables
{ 
    [CreateAssetMenu(menuName = "Variables/Float Variable")]
    public class FloatVariable : BaseVariable<float>
    {
        public void SetValue(FloatVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(float amount)
        {
            Value += amount;
        }

        public void ApplyChange(FloatVariable amount)
        {
            Value += amount.Value;
        }
    }
}