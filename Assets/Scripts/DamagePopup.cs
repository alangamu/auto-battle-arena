using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField]
        private GameObject damageTextPrefab;
        [SerializeField]
        private ColorVariable _normalHitColor;
        [SerializeField]
        private ColorVariable _criticalHitColor;
        [SerializeField]
        private ColorVariable _healColor;

        private ICombatController _combatController;

        private void OnEnable()
        {
            if (transform.parent.TryGetComponent(out _combatController))
            {
                _combatController.OnGetHit += CombatControllerOnGetHit;
            }
        }

        private void OnDisable()
        {
            if (_combatController != null)
            {
                _combatController.OnGetHit -= CombatControllerOnGetHit;
            }
        }

        private void CombatControllerOnGetHit(int damage, bool isCritical)
        {
            GameObject damageTextObject = Instantiate(damageTextPrefab, transform);
            //damageTextObject.transform.parent = null;
            if (damageTextObject.TryGetComponent(out TMP_Text damageText))
            {
                if (damage < 0)
                {
                    damageText.text = (damage * -1).ToString();
                    damageText.color = _healColor.Value;
                }
                else
                {
                    damageText.text = damage.ToString();
                    damageText.color = isCritical ? _criticalHitColor.Value : _normalHitColor.Value;
                }
            }
            Destroy(damageTextObject, .5f);
            LeanTween.moveLocalY(damageTextObject, 100f, .2f);
            LeanTween.moveLocalX(damageTextObject, Random.Range(-100f, 100f) , .2f);
            LeanTween.scale(damageTextObject, Vector3.one * 3, .2f);
        }
    }
}