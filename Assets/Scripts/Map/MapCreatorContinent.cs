using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapCreatorContinent : MapCreator
    {
        [SerializeField]
        private GameObject _shorePrefab;
        [SerializeField]
        private GameObject _cornerNoWaterPrefab;
        [SerializeField]
        private GameObject _cornerWithWaterPrefab;

        public override void GenerateMap()
        {
            base.GenerateMap();
            int numRows = _mapNumRows.Value;
            int numColumns = _mapNumColumns.Value;

            Random.InitState(0);

            int numSplats = Random.Range(4, 10);
            for (int i = 0; i < numSplats; i++)
            {
                //create a sigle land mass
                int range = Random.Range(5, 8);
                int y = Random.Range((numColumns / 2) - 8, (numColumns / 2) + 8);
                int x = Random.Range((numColumns / 2) - 5, (numColumns / 2) + 5);

                ElevateArea(x, y, range);
            }

            float noiseResolution = 0.01f;
            Vector2 noiseOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
            float noiseScale = 2f;

            for (int column = 0; column < numColumns; column++)
            {
                for (int row = 0; row < numRows; row++)
                {
                    Hex h = _tileRuntimeSet.GetHexAt(column, row);
                    float n =
                        Mathf.PerlinNoise(((float)column / Mathf.Max(numColumns, numRows) / noiseResolution) + noiseOffset.x,
                        ((float)row / Mathf.Max(numColumns, numRows) / noiseResolution) + noiseOffset.y)
                        - 0.5f;
                    h.AddElevation(n * noiseScale);
                }
            }

            noiseResolution = 0.05f;
            noiseOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

            noiseScale = 2f;  // Larger values makes more islands (and lakes, I guess)

            for (int column = 0; column < numColumns; column++)
            {
                for (int row = 0; row < numRows; row++)
                {
                    Hex h = _tileRuntimeSet.GetHexAt(column, row);
                    float n =
                        Mathf.PerlinNoise(((float)column / Mathf.Max(numColumns, numRows) / noiseResolution) + noiseOffset.x,
                            ((float)row / Mathf.Max(numColumns, numRows) / noiseResolution) + noiseOffset.y)
                        - 0.5f;
                    h.SetMoisture(n * noiseScale);
                }
            }

            _updateMapVisuals.Raise();
            //ShowBorders();
            List<HexBase> map = new List<HexBase>();
            //HexBase[,] map = new HexBase[numColumns, numRows];
            foreach (var item in _tileRuntimeSet.Items)
            {
                Hex hex = item.GetHex();
                HexBase hexBase = new HexBase
                {
                    q = hex.Q,
                    r = hex.R,
                    moisture = hex.Moisture,
                    elevation = hex.Elevation,
                    tileState = _initialTileState
                };
                map.Add(hexBase);
                //map[hex.Q, hex.R] = hexBase;
                //_tileRuntimeSet.Map[hex.Q,hex.R] = hexBase;
            }
            _tileRuntimeSet.Map = map;
        }

        //private void ShowBorders()
        //{
        //    //TODO: make a prefabs of the borders so not every tile loads inactive game objects
        //    // and in the prefab, spawn only the ones needed. This is for performance
        //    List<ITile> tiles = _tileRuntimeSet.Items.FindAll(x => x.GetHex().Elevation < 0f);

        //    foreach (ITile tile in tiles)
        //    {
        //        MapTileBordersController mapTileBordersController;
        //        Hex centerHex = tile.GetHex();
        //        if (tile.GetGameObject().TryGetComponent(out mapTileBordersController))
        //        {
        //            mapTileBordersController.ShowBorders(_shorePrefab);
        //        }
        //        if (centerHex != null)
        //        {
        //            //top
        //            if (centerHex.TopLeftNeighbourHex != null && centerHex.TopRightNeighbourHex != null)
        //            {
        //                if (!centerHex.TopLeftNeighbourHex.IsBorderReady && !centerHex.TopRightNeighbourHex.IsBorderReady)
        //                {
        //                    mapTileBordersController.ShowTopCorner(_cornerNoWaterPrefab, _cornerWithWaterPrefab);
        //                }
        //            }
        //            //top right
        //            if (centerHex.TopRightNeighbourHex != null)
        //            {
        //                if (!centerHex.RightNeighbourHex.IsBorderReady && !centerHex.TopRightNeighbourHex.IsBorderReady)
        //                {
        //                    mapTileBordersController.ShowTopRightCorner(_cornerNoWaterPrefab, _cornerWithWaterPrefab);
        //                }
        //            }
        //            //bot right
        //            if (centerHex.BotRightNeighbourHex != null)
        //            {
        //                if (!centerHex.RightNeighbourHex.IsBorderReady && !centerHex.BotRightNeighbourHex.IsBorderReady)
        //                {
        //                    mapTileBordersController.ShowBotRightCorner(_cornerNoWaterPrefab, _cornerWithWaterPrefab);
        //                }
        //            }
        //            //bot
        //            if (centerHex.BotLeftNeighbourHex != null && centerHex.BotRightNeighbourHex != null)
        //            {
        //                if (!centerHex.BotLeftNeighbourHex.IsBorderReady && !centerHex.BotRightNeighbourHex.IsBorderReady)
        //                {
        //                    mapTileBordersController.ShowBotCorner(_cornerNoWaterPrefab, _cornerWithWaterPrefab);
        //                }
        //            }
        //            //bot left
        //            if (centerHex.BotLeftNeighbourHex != null && centerHex.LeftNeighbourHex != null)
        //            {
        //                if (!centerHex.BotLeftNeighbourHex.IsBorderReady && !centerHex.LeftNeighbourHex.IsBorderReady)
        //                {
        //                    mapTileBordersController.ShowBotLeftCorner(_cornerNoWaterPrefab, _cornerWithWaterPrefab);
        //                }
        //            }
        //            //top left
        //            if (centerHex.TopLeftNeighbourHex != null && centerHex.LeftNeighbourHex != null)
        //            {
        //                if (!centerHex.TopLeftNeighbourHex.IsBorderReady && !centerHex.LeftNeighbourHex.IsBorderReady)
        //                {
        //                    mapTileBordersController.ShowTopLeftCorner(_cornerNoWaterPrefab, _cornerWithWaterPrefab);
        //                }
        //            }
        //        }

        //        centerHex.SetBordersReady();
        //    }
        //}

        private void ElevateArea(int q, int r, int range, float centerHeight = .8f)
        {
            Hex centerHex = _tileRuntimeSet.GetHexAt(q, r);

            Hex[] areaHexes = _tileRuntimeSet.GetHexesWithinRangeOf(centerHex, range);

            foreach (var item in areaHexes)
            {
                float elevation = centerHeight * Mathf.Lerp(1f, 0.25f, Mathf.Pow(Hex.Distance(centerHex, item) / range, 2f));
                item.SetElevation(elevation);
            }
        }
    }
}