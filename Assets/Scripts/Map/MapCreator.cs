using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField]
        protected GameEvent _updateMapVisuals;
        [SerializeField]
        protected IntVariable _mapNumRows;
        [SerializeField]
        protected IntVariable _mapNumColumns;
        [SerializeField]
        private FloatVariable _hexRadius;
        [SerializeField]
        protected TileRuntimeSet _tileRuntimeSet;

        [SerializeField]
        private GameObject _hexPrefab; // Prefab del hexágono

        virtual public void GenerateMap()
        {
            print("generate map");
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
                    Hex h = new Hex(column, row, _hexRadius.Value);
                    h.SetElevation(-0.5f);


                    Vector3 pos = new Vector3((column * hexSize) + rowOffset, 0f, row * hexSize * 0.866f);


                    //Vector3 pos = h.PositionFromCamera(
                    //    Camera.main.transform.position,
                    //    numColumns
                    //);

                    GameObject hexGO = Instantiate(
                        _hexPrefab,
                        pos,
                        Quaternion.identity,
                        transform
                    );

                    if (hexGO.TryGetComponent(out ITile tile))
                    {
                        tile.SetHex(h);
                        //tile.SetCoordinates(column, row);
                    }

                    //TODO: delete this, only for testing
                    if (hexGO.TryGetComponent(out MapTileIndicators mapTileIndicators))
                    {
                        mapTileIndicators.Initialize(column, row);
                    }

                    hexGO.name = $"Hex: {column}, {row}";
                }
            }
            _tileRuntimeSet.SetNeighbours();
        }

        private void Start()
        {
            GenerateMap();
        }
    }
}