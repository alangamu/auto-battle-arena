using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.Map;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu]
    public class TileRuntimeSet : RuntimeSet<ITile>
    {
        [SerializeField]
        private IntVariable _mapNumColumns;

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

        public Hex GetHexAt(int x, int y)
        {
            x %= _mapNumColumns.Value;
            if (x < 0)
            {
                x += _mapNumColumns.Value;
            }

            ITile tile = Items.Find(z => z.GetHex().Q == x && z.GetHex().R == y);
            return tile.GetHex();
        }

        //TODO: check neighbors of the first and last tile
        public void SetNeighbours()
        {
            foreach (var item in Items)
            {
                Hex centerHex = item.GetHex();
                // find the top right neighbour
                ITile tile = Items.Find(x => x.GetHex().Q == centerHex.Q && x.GetHex().R == centerHex.R + 1);
                if (tile != null)
                {
                    centerHex.SetTopRightNeighbourHex(tile.GetHex());
                }
                // find the bot left neighbour
                tile = Items.Find(x => x.GetHex().Q == centerHex.Q && x.GetHex().R == centerHex.R - 1);
                if (tile != null)
                {
                    centerHex.SetBotLeftNeighbourHex(tile.GetHex());
                }
                // find the bot right nighbour
                int q = centerHex.Q + 1;
                q %= _mapNumColumns.Value;
                if (q < 0)
                {
                    q += _mapNumColumns.Value;
                }
                tile = Items.Find(x => x.GetHex().Q == q && x.GetHex().R == centerHex.R - 1);
                if (tile != null)
                {
                    centerHex.SetBotRightNeighbourHex(tile.GetHex());
                }
                // find the right nighbour
                tile = Items.Find(x => x.GetHex().Q == q && x.GetHex().R == centerHex.R);
                if (tile != null)
                {
                    centerHex.SetRightNeighbourHex(tile.GetHex());
                }
                // find the top left nighbour
                q = centerHex.Q - 1;
                q %= _mapNumColumns.Value;
                if (q < 0)
                {
                    q += _mapNumColumns.Value;
                }
                tile = Items.Find(x => x.GetHex().Q == q && x.GetHex().R == centerHex.R + 1);
                if (tile != null)
                {
                    centerHex.SetTopLeftNeighbourHex(tile.GetHex());
                }
                // find the left nighbour
                tile = Items.Find(x => x.GetHex().Q == q && x.GetHex().R == centerHex.R);
                if (tile != null)
                {
                    centerHex.SetLeftNeighbourHex(tile.GetHex());
                }
            }
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

                //current.SetColor(ClosedColor);

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

                    Debug.Log(path.Count);
                    return path;
                }

                foreach (var neighbor in current.Neighbors.Where(t => t.IsWalkable && !processed.Contains(t)))
                {
                    var inSearch = toSearch.Contains(neighbor);

                    var costToNeighbor = current.G + Hex.Distance(current, neighbor); //current.GetDistance(neighbor);

                    if (!inSearch || costToNeighbor < neighbor.G)
                    {
                        neighbor.SetG(costToNeighbor);
                        neighbor.SetConnection(current);

                        if (!inSearch)
                        {
                            neighbor.SetH(Hex.Distance(neighbor, targetNode));//neighbor.GetDistance(targetNode));
                            toSearch.Add(neighbor);
                        }
                    }
                }
            }
            return null;
        }

        public List<Hex> GetNeighbours(Hex centerHex)
        {
            List<Hex> result = new List<Hex>();

            // find the top right neighbour
            ITile tile = Items.Find(x => x.GetHex().Q == centerHex.Q && x.GetHex().R == centerHex.R + 1);
            if (tile != null)
            {
                result.Add(tile.GetHex());
            }
            // find the bot left neighbour
            tile = Items.Find(x => x.GetHex().Q == centerHex.Q && x.GetHex().R == centerHex.R - 1);
            if (tile != null)
            {
                result.Add(tile.GetHex());
            }
            // find the bot right nighbour
            int q = centerHex.Q + 1;
            tile = Items.Find(x => x.GetHex().Q == q && x.GetHex().R == centerHex.R - 1);
            if (tile != null)
            {
                result.Add(tile.GetHex());
            }
            // find the right nighbour
            tile = Items.Find(x => x.GetHex().Q == q && x.GetHex().R == centerHex.R);
            if (tile != null)
            {
                result.Add(tile.GetHex());
            }
            // find the top left nighbour
            q = centerHex.Q - 1;
            tile = Items.Find(x => x.GetHex().Q == q && x.GetHex().R == centerHex.R + 1);
            if (tile != null)
            {
                result.Add(tile.GetHex());
            }
            // find the left nighbour
            tile = Items.Find(x => x.GetHex().Q == q && x.GetHex().R == centerHex.R);
            if (tile != null)
            {
                result.Add(tile.GetHex());
            }

            return result;
        }
    }
}