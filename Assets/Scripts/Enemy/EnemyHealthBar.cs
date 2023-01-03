using AutoFantasy.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image healthBarSprite;
        [SerializeField]
        private float reduceSpeed = 2;

        private IHealthController _healthController;
        private float _target = 1f;

        private void OnEnable()
        {
            if (TryGetComponent(out _healthController))
            {
                _healthController.OnHealthChange += HealthControllerOnHealthChange;
            }
        }

        private void OnDisable()
        {
            if (_healthController != null)
            {
                _healthController.OnHealthChange -= HealthControllerOnHealthChange;
            }
        }

        private void HealthControllerOnHealthChange(float newHealth)
        {
            _target = newHealth;
        }

        private void Update()
        {
            healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, _target, reduceSpeed * Time.deltaTime);
        }
    }
}