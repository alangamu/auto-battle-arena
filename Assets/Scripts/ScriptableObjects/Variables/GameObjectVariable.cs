using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Variables
{
    [CreateAssetMenu]
    public class GameObjectVariable : ScriptableObject
    {
        public GameObject Value => _gameObject;

        [SerializeField]
        private GameObject _gameObject;

        public void SetActiveGameObject(GameObject activeGameObject)
        {
            _gameObject = activeGameObject;
        }
    }
}