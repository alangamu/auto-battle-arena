using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Transform _enemyVisualTransform;

        public void Initialize(GameObject enemyPrefab)
        {
            Instantiate(enemyPrefab, _enemyVisualTransform);
        }
    }
}