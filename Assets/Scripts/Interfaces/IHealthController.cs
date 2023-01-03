using AutoFantasy.Scripts.ScriptableObjects;
using System;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IHealthController 
    {
        event Action OnDeath;
        event Action<float> OnHealthChange;

        void SetDificulty(MissionDifficultySO missionDifficulty);
    }
}