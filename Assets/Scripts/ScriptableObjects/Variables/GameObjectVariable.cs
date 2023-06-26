using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Variables
{
    [CreateAssetMenu]
    public class GameObjectVariable : ScriptableObject
    {
        public event Action<GameObject> OnValueChanged;
        public GameObject Value => _gameObject;

        [SerializeField]
        private GameObject _gameObject;

        public void SetActiveGameObject(GameObject activeGameObject)
        {
            _gameObject = activeGameObject;
            OnValueChanged?.Invoke(_gameObject);
        }
    }
}