using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
//using AutoFantasy.Scripts.ScriptableObjects.Variables;
//using AutoFantasy.Scripts.UI.Mission;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField]
        private HeroCombatRuntimeSet heroCombatSet;
        [SerializeField]
        private HeroCombatRuntimeSet enemyCombatSet;

        [SerializeField]
        private GameEvent heroDefeatEvent;
        [SerializeField]
        private GameEvent heroWinEvent;

        private void OnEnable()
        {
            heroCombatSet.OnHeroCombatEmpty += HeroCombatSet_OnHeroCombatEmpty;
            enemyCombatSet.OnHeroCombatEmpty += EnemyCombatSet_OnHeroCombatEmpty;
        }

        private void OnDisable()
        {
            heroCombatSet.OnHeroCombatEmpty -= HeroCombatSet_OnHeroCombatEmpty;
            enemyCombatSet.OnHeroCombatEmpty -= EnemyCombatSet_OnHeroCombatEmpty;
        }

        private void EnemyCombatSet_OnHeroCombatEmpty()
        {
            heroWinEvent.Raise();
        }

        private void HeroCombatSet_OnHeroCombatEmpty()
        {
            print($"game over");
            heroDefeatEvent.Raise();
        }
    }
}