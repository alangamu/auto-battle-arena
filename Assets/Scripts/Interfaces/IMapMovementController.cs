using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IMapMovementController
    {
        Task DoMovement(List<ITile> path);
    }
}