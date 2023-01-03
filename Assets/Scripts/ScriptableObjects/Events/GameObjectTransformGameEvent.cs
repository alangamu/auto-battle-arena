using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "GameObjectTransformGameEvent", menuName = "Game Events/GameObjectTransformGameEvent")]
    public class GameObjectTransformGameEvent : BaseDualGameEvent<GameObject, Transform>
    {

    }
}