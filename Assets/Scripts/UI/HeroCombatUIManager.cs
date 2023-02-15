using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class HeroCombatUIManager : MonoBehaviour
    {
        [SerializeField]
        private ActiveHeroSO _activeCombatHero;
        [SerializeField]
        private ActiveHeroSO _activeCombatEnemy;
        //[SerializeField]
        //private Transform _heroContainer;

        private IHeroController heroController;

        public void Attack()
        {
            
        }

        public void Skill()
        {

        }

        public void Skip()
        {

        }

        private void OnEnable()
        {
            _activeCombatHero.OnHeroChanged += ChangeHero;
            TryGetComponent(out heroController);
        }

        private void OnDisable()
        {
            _activeCombatHero.OnHeroChanged -= ChangeHero;
        }

        private void ChangeHero()
        {
            if (heroController != null)
            {
                heroController.SetHero(_activeCombatHero.ActiveHero);
            }
        }
    }
}