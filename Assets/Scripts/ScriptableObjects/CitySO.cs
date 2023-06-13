using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class CitySO : ScriptableObject
    {
        public int Q => _q;
        public int R => _r;
        public string CityName => _cityName;
        public GameObject CityPrefab => _cityPrefab;

        [SerializeField]
        private int _q;
        [SerializeField]
        private int _r;
        [SerializeField]
        private string _cityName;
        [SerializeField]
        private GameObject _cityPrefab;
    }
}