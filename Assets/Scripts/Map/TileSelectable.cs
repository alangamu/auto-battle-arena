using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class TileSelectable : MonoBehaviour, ISelectable
    {
        public bool IsSelected => _isSelected;

        [SerializeField]
        private GameObject _tileSelectIndicator;

        private bool _isSelected;

        public void Select(bool option)
        {
            _isSelected = option;
            _tileSelectIndicator.SetActive(option);
        }
    }
}