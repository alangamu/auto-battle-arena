using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class Arrow : MonoBehaviour, IProjectile
    {
        //TODO: make a arrow reference in the weaponSO
        public void Launch(Transform targetTransform)
        {
            transform.LookAt(targetTransform);
            LeanTween.move(gameObject, targetTransform, .1f);
            transform.SetParent(targetTransform);
            Destroy(gameObject, 1f);
        }
    }
}