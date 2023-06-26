using AutoFantasy.Scripts.Map;
using AutoFantasy.Scripts.ScriptableObjects.Map;
using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface ITile 
    {
        public TileStateSO TileState { get; }
        //public void SetCoordinates(int q, int r); 
        public GameObject GetGameObject();
        public void SetHex(Hex hex);
        public Hex GetHex();
        public void SetState(TileStateSO tileState);
    }
}