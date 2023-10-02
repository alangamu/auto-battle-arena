using AutoFantasy.Scripts.Map;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Map;
using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface ITile 
    {
        public TileStateSO TileState { get; }
        public TileTypeSO TileType { get; }
        public GameObject GetGameObject();
        public void SetHex(Hex hex);
        public Hex GetHex();
        public void SetState(TileStateSO tileState);
        public void SetType(TileTypeSO tileType);
        public Transform GetActiveTile();
    }
}