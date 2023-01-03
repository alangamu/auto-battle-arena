using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI.Mission
{
    public class MissionTabButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private GameEvent showMissionTabEvent;

        private void OnEnable()
        {
            button.onClick.AddListener(MissionsTabPressed);
        }

        private void OnDisable()
        {
            button.onClick.AddListener(MissionsTabPressed);
        }

        private void MissionsTabPressed()
        {
            showMissionTabEvent.Raise();
        }
    }
}