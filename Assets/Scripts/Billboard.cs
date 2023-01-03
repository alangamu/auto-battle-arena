using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class Billboard : MonoBehaviour
    {
        private Transform _camTransform;

        private void Start()
        {
            _camTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _camTransform.forward);
        }
    }
}