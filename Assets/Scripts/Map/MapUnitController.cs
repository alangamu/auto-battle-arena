using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapUnitController : MonoBehaviour, IMapUnitController
    {
        [SerializeField]
        private GameObjectRuntimeSet _heroesGORuntimeSet;

        private int _q;
        private int _r;

        public int Q => _q;

        public int R => _r;

        public void SetHexCoordinates(int q, int r)
        {
            _q = q;
            _r = r;
        }

        private void OnEnable()
        {
            _heroesGORuntimeSet.Add(gameObject);
        }

        private void OnDisable()
        {
            _heroesGORuntimeSet.Remove(gameObject);
        }
    }
}