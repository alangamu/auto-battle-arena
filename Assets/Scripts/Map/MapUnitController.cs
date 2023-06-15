using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapUnitController : MonoBehaviour, IMapUnitController
    {
        private int _q;
        private int _r;

        public int GetHexCoordinatesQ() => _q;

        public int GetHexCoordinatesR() => _r;

        public void SetHexCoordinates(int q, int r)
        {
            _q = q;
            _r = r;
        }
    }
}