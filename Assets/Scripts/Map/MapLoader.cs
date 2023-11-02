using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapLoader : MonoBehaviour
    {
        [SerializeField]
        private TileRuntimeSet _tileRuntimeSet;
        [SerializeField]
        private StringVariable _mapJson;
        [SerializeField]
        private GameEvent _loadMapEvent;
        [SerializeField]
        private GameObject _hexPrefab;
        [SerializeField]
        protected GameEvent _updateMapVisuals;

        private void OnEnable()
        {
            _loadMapEvent.OnRaise += LoadMap;
        }

        private void OnDisable()
        {
            _loadMapEvent.OnRaise -= LoadMap;
        }

        private void LoadMap()
        {
            print($"Load map");
            //JsonUtility.FromJsonOverwrite(_mapJson.Value, _tileRuntimeSet);
            float hexSize = 5f;

            foreach (var item in _tileRuntimeSet.Map)
            {
                float rowOffset = 0f;
                if (item.r % 2 != 0)
                {
                    rowOffset = hexSize / 2;
                }

                Hex h = new Hex(item.q, item.r);
                h.SetMoisture(item.moisture);
                h.SetElevation(item.elevation);
                Vector3 pos = new Vector3((item.q * hexSize) + rowOffset, 0f, item.r * hexSize * 0.866f);

                GameObject hexGO = Instantiate(
                    _hexPrefab,
                    pos,
                    Quaternion.identity,
                    transform
                );
                if (hexGO.TryGetComponent(out ITile tile))
                {
                    tile.SetHex(h);
                }

                //TODO: delete this, only for testing
                if (hexGO.TryGetComponent(out MapTileIndicators mapTileIndicators))
                {
                    mapTileIndicators.Initialize(item.q, item.r);
                }

                hexGO.name = $"Hex: {item.q}, {item.r}";
            }

            _updateMapVisuals.Raise();
            //_tileRuntimeSet.SetTileState(_initialTileState, _tileRuntimeSet.Items);

        }
    }
}