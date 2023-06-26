using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using HighlightPlus;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class RightClickMouseIndicator : MonoBehaviour
    {
        [SerializeField]
        private GameObjectGameEvent _selectTileEvent;
        [SerializeField]
        private GameObject _reactingMesh;

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (gameObject.TryGetComponent(out ITile tile))
                {
                    if (_reactingMesh.TryGetComponent(out HighlightEffect highlight))
                    {
                        highlight.HitFX();
                    }
                    _selectTileEvent.Raise(gameObject);
                }
            }
        }
    }
}