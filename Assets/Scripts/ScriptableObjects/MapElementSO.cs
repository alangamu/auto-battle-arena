using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    public class MapElementSO : ScriptableObject, IMapUnitController
    {
        public int Q => _q;
        public int R => _r;

        [SerializeField]
        private int _q;
        [SerializeField]
        private int _r;

        public void SetHexCoordinates(int q, int r)
        {
            _q = q;
            _r = r;
        }
    }
}