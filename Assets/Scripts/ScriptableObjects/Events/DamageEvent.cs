using System;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "DamageEvent", menuName = "Game Events/DamageEvent", order = 0)]
    public class DamageEvent : ScriptableObject
    {
        public event Action<GameObject, GameObject, int> OnRaise;

        public void Raise(GameObject attacker, GameObject attacked, int damage)
        {
            OnRaise?.Invoke(attacker, attacked, damage);
        }
    }
}