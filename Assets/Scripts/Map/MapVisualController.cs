using AutoFantasy.Scripts.Enemy;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Map;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
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

        [SerializeField]
        private GameObject _cityPrefab;
        [SerializeField]
        private IntVariable _cityCount;

        [SerializeField]
        private GameObject _enemyPrefab;

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

                //if (hexElevation >= _heightFlat && hexElevation < _heightMountains)
                //{
                if (hexMoisture >= _moistureJungle)
                {
                    mr.material = _matGrasslands;

                // Spawn trees
                //Vector3 p = hexGO.transform.position;
                //if (hexElevation >= _heightHill)
                //{
                //    p.y += _tilePrefabTileHeight;
                //}
                    if (tileTerrainType.CanHaveTrees)
                    {
                        Instantiate(_forestPrefab, hexGO.transform);
                    }
                }
                else if (hexMoisture >= _moistureForest)
                {
                    mr.material = _matGrasslands;

                // Spawn trees
                //Vector3 p = hexGO.transform.position;
                //if (hexElevation >= _heightHill)
                //{
                //    p.y += _tilePrefabTileHeight;
                //}
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
//                }

//                TileTerrainTypeSO tileTerrainType = _tileTerrainTypes.Find(x => x.HeightPrev < hexElevation && x.HeightLimit >= hexElevation);

                mf.mesh = tileTerrainType.TerrainMeshes[Random.Range(0, tileTerrainType.TerrainMeshes.Length)];

                //if (hexElevation >= _heightMountains)
                //{
                //    hexGO.transform.position += new Vector3(0f, _tilePrefabTileHeight, 0f);
                //    mf.mesh = _meshesMountains[Random.Range(0, _meshesMountains.Length)];
                //    //hex.SetTileTerrainType(_tileTerrainTypes.Find(x => x.));
                //}
                //else if (hexElevation >= _heightHill)
                //{
                //    hexGO.transform.position += new Vector3(0f, _tilePrefabTileHeight, 0f);
                //    mf.mesh = _meshHill;
                //}
                //else if (hexElevation >= _heightFlat)
                //{
                //    hexGO.transform.position += new Vector3(0f, _tilePrefabTileHeight, 0f);
                //    mf.mesh = _meshFlat;
                //}
                //else
                //{
                //    mr.material = _matOcean;
                //    mf.mesh = _meshWater;
                //}

                mf.transform.Rotate(new Vector3(0f, Random.Range(0, 4) * 60f, 0f));
                hex.SetIsWalkable(tileTerrainType.IsWalkable);
            }

            UpdateIslands();

            // SpawnInitialCity();
            SpawnCities();
            SpawnEnemies();
        }

        //TODO: move from here with a GameEvent
        private void SpawnEnemies() 
        {
            EnemySO[] enemies = Resources.LoadAll<EnemySO>("Enemies");

            foreach (var item in enemies)
            {
                GameObject enemy = Instantiate(_enemyPrefab, _tiles.Items.Find(x => x.GetHex().Q == item.Q && x.GetHex().R == item.R).GetGameObject().transform);

                if (enemy.TryGetComponent(out EnemyController enemyController))
                {
                    enemyController.Initialize(item.EnemyVisualPrefab);
                }

                enemy.transform.Rotate(new Vector3(0, 180, 0));
            }
        }


        private void SpawnCities()
        {
            CitySO[] cities = Resources.LoadAll<CitySO>("Cities");

            foreach (var item in cities)
            {
                Instantiate(item.CityPrefab, _tiles.Items.Find(x => x.GetHex().Q == item.Q && x.GetHex().R == item.R).GetGameObject().transform);
            }

            //List<ITile> walkableTiles = _tiles.Items.FindAll(x => x.GetHex().IsWalkable);
            //List<ITile> tiles = walkableTiles.FindAll(x => x.GetHex().R > 6);

            //for (int i = 0; i < _cityCount.Value; i++)
            //{
            //    if (tiles != null && tiles.Count > 0)
            //    {
            //        ITile tile = tiles[Random.Range(0, tiles.Count)];
            //        Instantiate(_cityPrefab, tile.GetGameObject().transform);
            //        print($"city tile {tile.GetHex().Q}, {tile.GetHex().R}");
            //    }
            //}
        }

        private void SpawnInitialCity()
        {
            List<ITile> walkableTiles = _tiles.Items.FindAll(x => x.GetHex().IsWalkable);
            List<ITile> tiles = walkableTiles.FindAll(x => x.GetHex().R <= 5);

            if (tiles != null && tiles.Count > 0)
            {
                ITile tile = tiles[Random.Range(0, tiles.Count)];
                Instantiate(_cityPrefab, tile.GetGameObject().transform);
                print($"first city tile {tile.GetHex().Q}, {tile.GetHex().R}");
            }
        }

        private void UpdateIslands()
        {
            List<ITile> tiles = _tiles.Items.FindAll(x => x.GetHex().IsWalkable);
            foreach (var item in tiles)
            {
                if (item.GetHex().Neighbors.FindAll(y => y.IsWalkable).Count == 0)
                {
                    item.GetHex().SetIsWalkable(false);
                }
            }
        }
    }
}