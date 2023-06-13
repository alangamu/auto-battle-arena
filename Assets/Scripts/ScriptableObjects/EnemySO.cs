using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class EnemySO : ScriptableObject
    {
        public GameObject EnemyVisualPrefab => _enemyVisual;
        public int Q => _q;
        public int R => _r;
        public WeaponSO Weapon => _weapon;

        [SerializeField] 
        private GameObject _enemyVisual;
        [SerializeField]
        private int _q;
        [SerializeField]
        private int _r;
        [SerializeField]
        private WeaponSO _weapon;
    }
}