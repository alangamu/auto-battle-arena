using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IMapUnitController 
    {
        int GetHexCoordinatesQ();
        int GetHexCoordinatesR();
        void SetHexCoordinates(int q, int r);
    }
}