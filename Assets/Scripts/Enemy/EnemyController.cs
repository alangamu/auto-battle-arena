using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Transform _enemyVisualTransform;

        public GameObject Initialize(GameObject enemyPrefab)
        {
            GameObject enemyObject = Instantiate(enemyPrefab, _enemyVisualTransform);
            return enemyObject;
        }
    }
}