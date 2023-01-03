using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image healthBarSprite;

        [SerializeField]
        private float reduceSpeed = 2;

        [SerializeField]
        private GameObjectFloatGameEvent heroChangeHealth;

        private float _target = 1;

        private void OnEnable()
        {
            heroChangeHealth.OnRaise += HeroChangeHealth_OnRaise;
        }

        private void OnDisable()
        {
            heroChangeHealth.OnRaise -= HeroChangeHealth_OnRaise;
        }

        private void HeroChangeHealth_OnRaise(GameObject heroGameObject, float newHealth)
        {
            if (heroGameObject == transform.parent.gameObject)
            {
                _target = newHealth;
                if (newHealth == 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        private void Update()
        {
            healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, _target, reduceSpeed * Time.deltaTime);
        }
    }
}