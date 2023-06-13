using AutoFantasy.Scripts.Map;
using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface ITile 
    {
        //public void SetCoordinates(int q, int r); 
        public GameObject GetGameObject();
        public void SetHex(Hex hex);
        public Hex GetHex();
    }
}