using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class HeroCounterUI : MonoBehaviour
    {
        [SerializeField]
        private HeroRuntimeSet heroesList;
        [SerializeField]
        private IntVariable maxHeroesCount;
        [SerializeField]
        private TMP_Text countText;

        private void OnEnable()
        {
            heroesList.OnChange += RefreshUI;
            RefreshUI();
        }

        private void OnDisable()
        {
            heroesList.OnChange -= RefreshUI;
        }

        private void RefreshUI()
        {
            countText.text = $"{heroesList.Items.Count} <#6f89a6>/ {maxHeroesCount.Value}";
        }
    }
}