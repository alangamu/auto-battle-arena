using System;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IHelmetController 
    {
        event Action OnWearHelmet;
        event Action OnTakeOffHelmet;

        void WearHelmet();
        void TakeOffHelmet();
    }
}