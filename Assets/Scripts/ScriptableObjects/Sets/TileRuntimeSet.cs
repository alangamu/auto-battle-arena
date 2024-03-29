﻿using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.Map;
using AutoFantasy.Scripts.ScriptableObjects.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu]
    public class TileRuntimeSet : RuntimeSet<ITile>
    {
        public List<HexBase> Map;
        //public HexBase[,] Map;

        //[SerializeField]
        //private IntVariable _mapNumColumns;

        public Hex[] GetHexesWithinRangeOf(Hex centerHex, int range)
        {
            List<Hex> result = new List<Hex>();

            for (int dx = -range; dx < range - 1; dx++)
            {
                for (int dy = Mathf.Max(-range + 1, -dx - range); dy < Mathf.Min(range, -dx + range - 1); dy++)
                {
                    result.Add(GetHexAt(centerHex.Q + dx, centerHex.R + dy));
                }
            }

            return result.ToArray();
        }

        public ITile GetTileAt(int q, int r)
        {
            ITile tile = Items.Find(z => z.GetHex().Q == q && z.GetHex().R == r);
            return tile;
        }

        public Hex GetHexAt(int x, int y)
        {
            ITile tile = GetTileAt(x, y);
            return tile.GetHex();
        }

        public List<Hex> GetNeighbors(Hex centerHex)
        {
            List<Hex> neighbors = new List<Hex>();
            int q = centerHex.Q;
            int r = centerHex.R;
            ITile tile;
            if (r % 2 == 0)
            {
                // find the top right neighbour
                tile = Items.Find(x => x.GetHex().Q == centerHex.Q && x.GetHex().R == centerHex.R + 1);
                if (tile != null)
                {
                    neighbors.Add(tile.GetHex());
                }
                // find the bot left neighbour
                tile = Items.Find(x => x.GetHex().Q == centerHex.Q - 1 && x.GetHex().R == centerHex.R - 1);
                if (tile != null)
                {
                    neighbors.Add(tile.GetHex());
                }
                // find the bot right nighbour
                tile = Items.Find(x => x.GetHex().Q == centerHex.Q && x.GetHex().R == centerHex.R - 1);
                if (tile != null)
                {
                    neighbors.Add(tile.GetHex());
                }
                // find the top left nighbour
                tile = Items.Find(x => x.GetHex().Q == centerHex.Q - 1 && x.GetHex().R == centerHex.R + 1);
                if (tile != null)
                {
                    neighbors.Add(tile.GetHex());
                }
            }
            else
            {
                // find the top right neighbour
                tile = Items.Find(x => x.GetHex().Q == centerHex.Q + 1 && x.GetHex().R == centerHex.R + 1);
                if (tile != null)
                {
                    neighbors.Add(tile.GetHex());
                }
                // find the bot left neighbour
                tile = Items.Find(x => x.GetHex().Q == centerHex.Q && x.GetHex().R == centerHex.R - 1);
                if (tile != null)
                {
                    neighbors.Add(tile.GetHex());
                }
                // find the bot right nighbour
                tile = Items.Find(x => x.GetHex().Q == centerHex.Q + 1 && x.GetHex().R == centerHex.R - 1);
                if (tile != null)
                {
                    neighbors.Add(tile.GetHex());
                }
                // find the top left nighbour
                tile = Items.Find(x => x.GetHex().Q == centerHex.Q && x.GetHex().R == centerHex.R + 1);
                if (tile != null)
                {
                    neighbors.Add(tile.GetHex());
                }
            }

            // find the right nighbour
            tile = Items.Find(x => x.GetHex().Q == centerHex.Q + 1 && x.GetHex().R == centerHex.R);
            if (tile != null)
            {
                neighbors.Add(tile.GetHex());
            }
            // find the left nighbour
            tile = Items.Find(x => x.GetHex().Q == centerHex.Q - 1 && x.GetHex().R == centerHex.R);
            if (tile != null)
            {
                neighbors.Add(tile.GetHex());
            }

            return neighbors;
        }

        public List<Hex> FindPath(Hex startNode, Hex targetNode)
        {
            var toSearch = new List<Hex>() { startNode };
            var processed = new List<Hex>();

            while (toSearch.Any())
            {
                var current = toSearch[0];
                foreach (var t in toSearch)
                    if (t.F < current.F || t.F == current.F && t.H < current.H) current = t;

                processed.Add(current);
                toSearch.Remove(current);

                if (current == targetNode)
                {
                    var currentPathTile = targetNode;
                    var path = new List<Hex>();
                    var count = 100;
                    while (currentPathTile != startNode)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                        count--;
                        if (count < 0) throw new Exception();
                    }

                    return path;
                }

                List<Hex> neighbors = GetNeighbors(current);

                foreach (var neighbor in neighbors.Where(t => t.IsWalkable && !processed.Contains(t)))
                {
                    var inSearch = toSearch.Contains(neighbor);

                    var costToNeighbor = current.G + Hex.Distance(current, neighbor); 

                    if (!inSearch || costToNeighbor < neighbor.G)
                    {
                        neighbor.SetG(costToNeighbor);
                        neighbor.SetConnection(current);

                        if (!inSearch)
                        {
                            neighbor.SetH(Hex.Distance(neighbor, targetNode));
                            toSearch.Add(neighbor);
                        }
                    }
                }
            }
            return null;
        }

        public List<ITile> GetNeighboursTiles(Hex centerHex, int range)
        {
            List<Hex> result = new()
            {
                centerHex
            };

            List<Hex> neighbors = new List<Hex>();
            for (int i = 0; i < range; i++)
            {
                neighbors = result;
                int neighborCount = neighbors.Count;
                for (int j = 0; j < neighborCount; j++)
                {
                    foreach (var neighbor in GetNeighbors(neighbors[j]))
                    {
                        if (!result.Contains(neighbor))
                        {
                            result.Add(neighbor);
                        }
                    }
                }
            }

            List<ITile> tiles = new List<ITile>();

            foreach (var item in result)
            {
                tiles.Add(GetTileAt(item.Q, item.R));
            }

            return tiles;
        }

        public List<ITile> GetNeighboursTiles(int q, int r, int range)
        {
            Hex hex = GetHexAt(q,r);
            return GetNeighboursTiles(hex, range);
        }
        public void SetTileState(TileStateSO tileState, List<ITile> tiles)
        {
            foreach(var tile in tiles)
            {
                tile.SetState(tileState);
            }
        }
    }
}