using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IProjectile 
    {
        void Launch(Transform targetTransform, float timeToImpact);
    }
}