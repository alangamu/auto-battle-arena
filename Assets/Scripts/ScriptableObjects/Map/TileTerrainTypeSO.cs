using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Map
{
    [CreateAssetMenu]
    public class TileTerrainTypeSO : ScriptableObject
    {
        public float HeightLimit => _heightLimit;
        public Mesh[] TerrainMeshes => _terrainMeshes;
        public float HeightPrev => _prevHeightTileTerrainType != null ? _prevHeightTileTerrainType.HeightLimit : -5f;
        public bool IsWalkable => _isWalkable;
        public bool CanHaveTrees => _canHaveTrees;

        [SerializeField]
        private float _heightLimit;
        [SerializeField]
        private TileTerrainTypeSO _prevHeightTileTerrainType;
        [SerializeField]
        private Mesh[] _terrainMeshes;
        [SerializeField]
        private bool _isWalkable;
        [SerializeField]
        private bool _canHaveTrees;
    }
}