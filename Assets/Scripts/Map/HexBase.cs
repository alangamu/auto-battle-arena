using AutoFantasy.Scripts.ScriptableObjects.Map;
using System;

namespace AutoFantasy.Scripts.Map
{
    [Serializable]
    public class HexBase
    {
        public int q;
        public int r;
        public float elevation;
        public float moisture;
        public TileStateSO tileState; 
    }
}