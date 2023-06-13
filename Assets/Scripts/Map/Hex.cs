using AutoFantasy.Scripts.ScriptableObjects.Map;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    [Serializable]
    public class Hex
    {
        static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
        public int Q => _q;
        public int R => _r;
        public int S => _s;
        public float Elevation => _elevation;
        public float Moisture => _moisture;

        public TileTerrainTypeSO TileTerrainType => _tileTerrainType;

        //        public bool IsBorderReady => _isBorderReady;

        private int _q;
        private int _r;
        private int _s;
        private float _elevation;
        private float _moisture;
        private float _radius;

        private TileTerrainTypeSO _tileTerrainType;

        //private bool _isBorderReady;

        private bool _isWalkable;
        private List<Hex> _neighbors;

        public bool IsWalkable => _isWalkable;
        public List<Hex> Neighbors => _neighbors;

        public Hex Connection { get; private set; }
        public float G { get; private set; }
        public float H { get; private set; }
        public float F => G + H;

        public void SetTileTerrainType(TileTerrainTypeSO tileTerrainType)
        {
            _tileTerrainType = tileTerrainType;
        }

        public void SetIsWalkable(bool isWalkable)
        {
            _isWalkable = isWalkable;
        }

        public void SetConnection(Hex nodeBase)
        {
            Connection = nodeBase;
        }

        public void SetG(float g)
        {
            G = g;
        }

        public void SetH(float h)
        {
            H = h;
        }

        public Hex(int q, int r, float radius)
        { 
            _radius = radius;
            _q = q;
            _r = r;
            _s = -(q + r);
            _neighbors = new List<Hex>();
            //_isBorderReady = false;
        }

        //public void SetBordersReady()
        //{
        //    _isBorderReady = true;
        //}

        public void SetTopRightNeighbourHex(Hex hex)
        {
            _neighbors.Add(hex);
        }

        public void SetRightNeighbourHex(Hex hex)
        {
            _neighbors.Add(hex);
        }

        public void SetBotRightNeighbourHex(Hex hex)
        {
            _neighbors.Add(hex);
        }

        public void SetBotLeftNeighbourHex(Hex hex)
        {
            _neighbors.Add(hex);
        }

        public void SetLeftNeighbourHex(Hex hex)
        {
            _neighbors.Add(hex);
        }

        public void SetTopLeftNeighbourHex(Hex hex)
        {
            _neighbors.Add(hex);
        }

        public static float Distance(Hex a, Hex b)
        {
            return
                Mathf.Max(
                    Mathf.Abs(a.Q - b.Q),
                    Mathf.Abs(a.R - b.R),
                    Mathf.Abs(a.S - b.S)
                );
        }

        public void SetElevation(float elevation)
        {
            _elevation = elevation;
        }

        public void AddElevation(float elevation)
        {
            _elevation += elevation;
        }

        public void SetMoisture(float moisture)
        {
            _moisture = moisture;
        }

        public Vector3 PositionFromCamera(Vector3 cameraPosition, int numColumns)
        {
            float mapWidth = numColumns * HexHorizontalSpacing();

            Vector3 position = Position();

            float howManyWidthsFromCamera = (position.x - cameraPosition.x) / mapWidth;

            if (Mathf.Abs(howManyWidthsFromCamera) <= 0.5f)
            {
                return position;
            }

            if (howManyWidthsFromCamera > 0)
            {
                howManyWidthsFromCamera += 0.5f;
            }
            else
            {
                howManyWidthsFromCamera -= 0.5f;
            }

            int howManyWidthToFix = (int)howManyWidthsFromCamera;

            position.x -= howManyWidthToFix * mapWidth;

            return position;
        }

        public float HexWidth()
        {
            return WIDTH_MULTIPLIER * HexHeight();
        }

        private float HexHorizontalSpacing()
        {
            return HexWidth();
        }

        private float HexHeight()
        {
            return _radius * 2;
        }

        private Vector3 Position()
        {
            return new Vector3(
                HexHorizontalSpacing() * (Q + R / 2f),
                0f,
                HexVerticalSpacing() * R
            );
        }

        private float HexVerticalSpacing()
        {
            return HexHeight() * 0.75f;
        }
    }
}