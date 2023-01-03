using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.UI.Mission
{
    public class BuyButtonMonitor : MonoBehaviour, IMonitor
    {
        [SerializeField]
        private IntVariable gold;
        [SerializeField]
        private GameObject buyButton;

        private int _monitoValue;

        private void OnEnable()
        {
            gold.OnChange += Gold_OnChange;
        }

        private void Gold_OnChange()
        {
            buyButton.SetActive(gold.Value >= _monitoValue);
        }

        private void OnDisable()
        {
            gold.OnChange -= Gold_OnChange;
        }

        public void SetValue(int value)
        {
            _monitoValue = value;
        }
    }
}