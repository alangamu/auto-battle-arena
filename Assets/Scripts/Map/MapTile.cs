using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Map;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapTile : MonoBehaviour, ITile
    {
        public TileStateSO TileState => _tileState;
        public TileTypeSO TileType => _tileType;
        public GameObject GetGameObject() => gameObject;
        public Hex GetHex() => _hex;

        [SerializeField]
        private TileRuntimeSet _tileRuntimeSet;
        [SerializeField]
        private Transform _activeTileTransform;

        [SerializeField]
        private Hex _hex;

        private TileStateSO _tileState;
        private TileTypeSO _tileType;

        public void SetHex(Hex hex)
        {
            _hex = hex;
        }

        private void OnEnable()
        {
            _tileRuntimeSet.Add(this);
        }

        private void OnDisable()
        {
            _tileRuntimeSet.Remove(this);
        }

        public void SetState(TileStateSO tileState)
        {
            _tileState = tileState;
        }

        public void SetType(TileTypeSO tileType)
        {
            _tileType = tileType;
        }

        public Transform GetActiveTile()
        {
            return _activeTileTransform;
        }
    }
}