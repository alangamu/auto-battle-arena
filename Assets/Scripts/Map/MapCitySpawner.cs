using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapCitySpawner : MonoBehaviour
    {
        [SerializeField]
        private TileRuntimeSet _tiles;
        [SerializeField]
        private GameEvent _spawnMapCitiesEvent;
        [SerializeField]
        private TileTypeSO _cityTyleType;

        private void OnEnable()
        {
            _spawnMapCitiesEvent.OnRaise += SpawnCities;
        }

        private void OnDisable()
        {
            _spawnMapCitiesEvent.OnRaise -= SpawnCities;
        }

        private void SpawnCities()
        {
            CitySO[] cities = Resources.LoadAll<CitySO>("Cities");

            foreach (var item in cities)
            {
                ITile tile = _tiles.Items.Find(x => x.GetHex().Q == item.Q && x.GetHex().R == item.R);
                GameObject city = Instantiate(item.CityPrefab, tile.GetActiveTile());
                city.name = item.name;
                tile.SetType(_cityTyleType);
            }
        }
    }
}