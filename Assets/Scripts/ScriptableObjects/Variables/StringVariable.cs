using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Variables
{
    [CreateAssetMenu(fileName = "StringVariable", menuName = "Variables/String Variable")]
    public class StringVariable : ScriptableObject
    {
        public string Value;
    }
}