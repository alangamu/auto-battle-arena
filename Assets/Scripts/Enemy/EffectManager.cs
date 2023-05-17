using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.Enemy
{
    public class EffectManager : MonoBehaviour, IEffectController
    {
        [SerializeField]
        private GameObject stepEffectPrefab;
        [SerializeField]
        private GameObject bloodEffectPrefab;
        [SerializeField]
        private GameObject _healEffectPrefab;

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
            if (damage < 0)
            {
                GameObject healEffect = Instantiate(_healEffectPrefab, transform.position + Vector3.up * 2f, Random.rotation, transform);
                Destroy(healEffect, 1f);
            }
            else
            {
                GameObject hitEffect = Instantiate(bloodEffectPrefab, transform.position + Vector3.up, Random.rotation, transform);
                Destroy(hitEffect, 1f);
            }
        }
    }
}