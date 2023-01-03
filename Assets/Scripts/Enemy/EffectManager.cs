using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EffectManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject stepEffectPrefab;
        [SerializeField]
        private GameObject bloodEffectPrefab;

        private ICombatController _combatController;

        public void FootR()
        {
            SpawnFootstep();
        }

        public void FootL()
        {
            SpawnFootstep();
        }

        private void SpawnFootstep()
        {
            GameObject stepEffect = Instantiate(stepEffectPrefab, transform);
            Destroy(stepEffect, 1f);
        }

        private void OnEnable()
        {
            if (TryGetComponent(out _combatController))
            {
                _combatController.OnGetHit += OnGetHit;
            }
        }

        private void OnDisable()
        {
            if (_combatController != null)
            {
                _combatController.OnGetHit -= OnGetHit;
            }
        }

        private void OnGetHit(int damage, bool isCritical)
        {
            GameObject stepEffect = Instantiate(bloodEffectPrefab, transform.position + Vector3.up, Random.rotation, transform);
            Destroy(stepEffect, 1f);
        }
    }
}