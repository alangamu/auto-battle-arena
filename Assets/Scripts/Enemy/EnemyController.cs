using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Transform _enemyVisualTransform;

        public GameObject Initialize(GameObject enemyPrefab)
        {
            return Instantiate(enemyPrefab, _enemyVisualTransform);
        }
    }
}