using AutoFantasy.Scripts.Heroes;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "GameObjectHeroGameEvent", menuName = "Game Events/GameObjectHeroGameEvent", order = 0)]
    public class GameObjectHeroGameEvent : BaseDualGameEvent<GameObject, Hero>
    {

    }
}