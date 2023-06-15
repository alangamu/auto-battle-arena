using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IMapMovementController
    {
        Task DoMovement(List<Vector3> path);
    }
}