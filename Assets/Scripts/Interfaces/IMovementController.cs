using System;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IMovementController 
    {
        event Action OnAttackTarget;
        event Action OnStartRunning;
        event Action OnSetIdle; 
    }
}