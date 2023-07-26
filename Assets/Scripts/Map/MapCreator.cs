using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Map;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField]
        protected GameEvent _generateMap;
        [SerializeField]
        protected GameEvent _updateMapVisuals;
        [SerializeField]
        protected IntVariable _mapNumRows;
        [SerializeField]
        protected IntVariable _mapNumColumns;
        [SerializeField]
        protected TileRuntimeSet _tileRuntimeSet;

        [SerializeField]
        private GameObject _hexPrefab;

        [SerializeField]
        private TileStateSO _initialTileState;

        virtual public void GenerateMap()
        {
            float hexSize = 5f;

            int numRows = _mapNumRows.Value;
            int numColumns = _mapNumColumns.Value;

            for (int row = 0; row < numRows; row++)
            {
                float rowOffset = 0f;
                if (row % 2 != 0)
                {
                    rowOffset = hexSize / 2;
                }
                for (int column = 0; column < numColumns; column++)
                {
                    Hex h = new Hex(column, row);
                    h.SetElevation(-0.5f);


                    Vector3 pos = new Vector3((column * hexSize) + rowOffset, 0f, row * hexSize * 0.866f);

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
                        mapTileIndicators.Initialize(column, row);
                    }

                    hexGO.name = $"Hex: {column}, {row}";
                }
            }
            _tileRuntimeSet.SetTileState(_initialTileState, _tileRuntimeSet.Items);
        }

        private void OnEnable()
        {
            _generateMap.OnRaise += GenerateMap;
        }

        private void OnDisable()
        {
            _generateMap.OnRaise -= GenerateMap;
        }
    }
}