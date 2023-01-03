using System.Collections;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EnemyTest : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyPrefab;

        private void Start()
        {
            Instantiate(enemyPrefab, transform);
        }
    }
}