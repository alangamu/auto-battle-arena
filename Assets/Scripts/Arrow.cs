using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class Arrow : MonoBehaviour, IProjectile
    {
        public void Launch(Transform targetTransform, float timeToImpact)
        {
            transform.LookAt(targetTransform);
            LeanTween.move(gameObject, targetTransform, timeToImpact);
            transform.SetParent(targetTransform);
            Destroy(gameObject, timeToImpact * 2);
        }
    }
}