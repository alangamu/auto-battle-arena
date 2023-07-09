using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Map;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapVisualController : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _updateMapVisuals;
        [SerializeField]
        private TileRuntimeSet _tiles;

        [SerializeField]
        private GameObject _junglePrefab;
        [SerializeField]
        private GameObject _forestPrefab;

        [SerializeField]
        private Mesh _meshWater;
        [SerializeField]
        private Mesh _meshFlat;
        [SerializeField]
        private Mesh _meshHill;

        [SerializeField]
        private Mesh[] _meshesMountains;
        [SerializeField]
        private Mesh[] _meshesIslands;

        [SerializeField]
        private Material _matOcean;
        [SerializeField]
        private Material _matPlains;
        [SerializeField]
        private Material _matGrasslands;
        [SerializeField]
        private Material _matMountains;
        [SerializeField]
        private Material _matDesert;

        [SerializeField]
        private List<TileTerrainTypeSO> _tileTerrainTypes;

        [SerializeField]
        private float _moistureJungle;
        [SerializeField]
        private float _moistureForest;
        [SerializeField]
        private float _moistureGrasslands;
        [SerializeField]
        private float _moisturePlains;

        private void OnEnable()
        {
            _updateMapVisuals.OnRaise += UpdateMapVisuals;
        }

        private void OnDisable()
        {
            _updateMapVisuals.OnRaise -= UpdateMapVisuals;
        }

        private void UpdateMapVisuals()
        {
            foreach (var h in _tiles.Items)
            {
                GameObject hexGO = h.GetGameObject();

                MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
                MeshFilter mf = hexGO.GetComponentInChildren<MeshFilter>();

                Hex hex = h.GetHex();
                float hexElevation = hex.Elevation;
                float hexMoisture = hex.Moisture;
                TileTerrainTypeSO tileTerrainType = _tileTerrainTypes.Find(x => x.HeightPrev < hexElevation && x.HeightLimit >= hexElevation);

                if (hexMoisture >= _moistureJungle)
                {
                    mr.material = _matGrasslands;

                // Spawn trees
                    if (tileTerrainType.CanHaveTrees)
                    {
                        Instantiate(_forestPrefab, hexGO.transform);
                    }
                }
                else if (hexMoisture >= _moistureForest)
                {
                    mr.material = _matGrasslands;

                // Spawn trees
                    if (tileTerrainType.CanHaveTrees)
                    {
                        Instantiate(_forestPrefab, hexGO.transform);
                    }
                }
                else if (hexMoisture >= _moistureGrasslands)
                {
                    mr.material = _matGrasslands;
                }
                else if (hexMoisture >= _moisturePlains)
                {
                    mr.material = _matPlains;
                }
                else
                {
                    mr.material = _matDesert;
                }

                mf.mesh = tileTerrainType.TerrainMeshes[Random.Range(0, tileTerrainType.TerrainMeshes.Length)];

                mf.transform.Rotate(new Vector3(0f, Random.Range(0, 4) * 60f, 0f));
                hex.SetIsWalkable(tileTerrainType.IsWalkable);
            }

            UpdateIslands();
        }

        private void UpdateIslands()
        {
            List<ITile> tiles = _tiles.Items.FindAll(x => x.GetHex().IsWalkable);
            foreach (var item in tiles)
            {

                if (_tiles.GetNeighbors(item.GetHex()).FindAll(y => y.IsWalkable).Count == 0)
                {
                    item.GetHex().SetIsWalkable(false);
                }
            }
        }
    }
}