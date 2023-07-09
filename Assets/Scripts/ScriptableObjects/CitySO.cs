using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class CitySO : MapElementSO
    {
        public GameObject CityPrefab => _cityPrefab;

        [SerializeField]
        private GameObject _cityPrefab;
    }
}