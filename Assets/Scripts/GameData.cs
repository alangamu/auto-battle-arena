using System;

namespace AutoFantasy.Scripts
{
    [Serializable]
    public class GameData
    {
        public string TilesJson;
        public string GameDate;
    }

    [Serializable]
    public class TileData
    {
        public int Q;
        public int R;
        public float Moisture;
        public float Elevation;
    }
}