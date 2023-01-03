using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoFantasy.Scripts
{
    public class MouseSelectable : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _buildingClick;

        private void OnMouseUpAsButton()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _buildingClick.Raise();
            }
        }
    }
}