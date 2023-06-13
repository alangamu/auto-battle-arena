using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapTile : MonoBehaviour, ITile
    {
        [SerializeField]
        private TileRuntimeSet _tileRuntimeSet;

        [SerializeField]
        private Hex _hex;

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Hex GetHex()
        {
            return _hex;
        }

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
    }
}