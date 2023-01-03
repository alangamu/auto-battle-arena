using AutoFantasy.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI.Mission
{
    public class MissionTabSelectorUI : MonoBehaviour, ISelectable
    {
        [SerializeField]
        private bool isOwnedMission;
        [SerializeField]
        private Image icon;
        [SerializeField]
        private GameObject selectedVisual;
        [SerializeField]
        private Color normalColor;
        [SerializeField]
        private Color selectedColor;
        [SerializeField]
        private TMP_Text titleText;

        public void Select(bool option)
        {
            if (option == isOwnedMission)
            {
                Select();
                return;
            }

            Deselect();
        }

        private void Deselect()
        {
            selectedVisual.SetActive(false);
            titleText.color = normalColor;
            icon.color = normalColor;
        }

        private void Select()
        {
            selectedVisual.SetActive(true);
            titleText.color = selectedColor;
            icon.color = selectedColor;
        }
    }
}