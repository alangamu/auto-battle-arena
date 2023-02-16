using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class HeroCombatUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _heroContainer;
        [SerializeField]
        private GameEvent _heroAttackEvent;
        [SerializeField]
        private ActiveHeroSO _activeHero;

        public void Attack()
        {
            _heroAttackEvent.Raise();
        }

        public void Skill()
        {

        }

        public void Skip()
        {

        }

        private void Start()
        {
            HidePanel();
        }

        private void OnEnable()
        {
            _activeHero.OnHeroChanged += ShowPanel;
            _heroAttackEvent.OnRaise += HidePanel;
        }

        private void OnDisable()
        {
            _activeHero.OnHeroChanged -= ShowPanel;
            _heroAttackEvent.OnRaise -= HidePanel;
        }

        private void ShowPanel()
        {
            _heroContainer.SetActive(true);
        }

        private void HidePanel()
        {
            _heroContainer.SetActive(false);
        }
    }
}