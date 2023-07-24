using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class EnemySO : ScriptableObject
    {
        public GameObject EnemyVisualPrefab => _enemyVisual;
        public WeaponSO Weapon => _weapon;
        public int MaxHealth => _maxHealth;
        //public Sprite EnemySprite => _enemySprite;

        [SerializeField] 
        private GameObject _enemyVisual;
        [SerializeField]
        private WeaponSO _weapon;
        //[SerializeField]
        //private Sprite _enemySprite;
        [SerializeField]
        private int _maxHealth;
    }
}