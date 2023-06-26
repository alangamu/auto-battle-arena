using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private GameEvent testEvent;
        [SerializeField]
        private TileRuntimeSet _tileRuntimeSet;
        [SerializeField]
        private GameObjectVariable _activeMapHero;

        [SerializeField]
        private TMP_Text _activeHeroSight;
        [SerializeField]
        private TMP_Text _activeHeroMovement;
        [SerializeField]
        private HeroStatSO _sightStat;
        [SerializeField]
        private HeroStatSO _movementStat;

        private void OnEnable()
        {
            _activeMapHero.OnValueChanged += UpdateStats;
            testEvent.OnRaise += TestEvent_OnRaise;
        }

        private void OnDisable()
        {
            testEvent.OnRaise -= TestEvent_OnRaise;
            _activeMapHero.OnValueChanged -= UpdateStats;
        }


        private void UpdateStats(GameObject newActiveHero)
        {
            if (newActiveHero != null)
            {
                if (newActiveHero.TryGetComponent(out IHeroController heroController))
                {
                    _activeHeroSight.text = $"Sight = {heroController.ThisHero.ThisCombatStats.StatCount(_sightStat.StatId)}";
                    _activeHeroMovement.text = $"Movement = {heroController.ThisHero.ThisCombatStats.StatCount(_movementStat.StatId)}";
                }
            }
        }

        private void TestEvent_OnRaise()
        {
        }
    }
}