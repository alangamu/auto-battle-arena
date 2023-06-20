using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class RightClickMouseIndicator : MonoBehaviour
    {
        [SerializeField]
        private GameObjectGameEvent _selectTileEvent;
        [SerializeField]
        private Material _normalMaterial;
        [SerializeField]
        private Material _clickedMaterial;
        [SerializeField]
        private MeshRenderer _renderer;

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonUp(1))
            {
                if(gameObject.TryGetComponent(out ITile tile))
                {
                    _renderer.material = _clickedMaterial;
                    _selectTileEvent.Raise(gameObject);
                    LeanTween.delayedCall(0.5f, () => { _renderer.material = _normalMaterial; });
                }
            }
        }
    }
}