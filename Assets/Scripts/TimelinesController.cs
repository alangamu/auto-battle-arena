using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.Playables;

namespace AutoFantasy.Scripts
{
    public class TimelinesController : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector _newRoundTimeline;
        [SerializeField]
        private GameEvent _newRoundEvent;

        private void OnEnable()
        {
            _newRoundEvent.OnRaise += NewRound;
        }

        private void NewRound()
        {
            _newRoundTimeline.Play();
        }
    }
}