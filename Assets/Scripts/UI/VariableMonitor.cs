using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class VariableMonitor : MonoBehaviour
    {
        [SerializeField]
        private IntVariable variable;
        [SerializeField]
        private TMP_Text variableText;

        private void OnEnable()
        {
            variable.OnChange += Variable_OnChange;
            Variable_OnChange();
        }

        private void OnDisable()
        {
            variable.OnChange -= Variable_OnChange;
        }

        private void Variable_OnChange()
        {
            variableText.text = variable.Value.ToString();
        }
    }
}