using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Variables
{
    [CreateAssetMenu(menuName = "Variables/Color Variable")]
    public class ColorVariable : ScriptableObject
    {
        public Color Value;
    }
}