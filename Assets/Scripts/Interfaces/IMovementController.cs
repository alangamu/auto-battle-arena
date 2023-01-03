using System;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IMovementController 
    {
        event Action OnReachTarget;
        event Action OnStartRunning;
    }
}