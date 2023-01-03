using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private FloatVariable timeRemaining;
        [SerializeField] private GameEvent onTimeRunOut;
        [SerializeField] private GameEvent startCountdown;
        [SerializeField] private TMP_Text timeText;

        private float _timeRemaining; 
        private bool _timerIsRunning;

        private void OnEnable()
        {
            _timerIsRunning = false;
            startCountdown.OnRaise += StartCountdown_OnRaise;
            DisplayTime(-1f);
        }

        private void OnDisable()
        {
            startCountdown.OnRaise -= StartCountdown_OnRaise;
        }

        private void StartCountdown_OnRaise()
        {
            if (!_timerIsRunning)
            {
                _timeRemaining = timeRemaining.Value;
                _timerIsRunning = true;
            }
        }

        private void Update()
        {
            if (_timerIsRunning)
            {
                if (_timeRemaining > 0)
                {
                    _timeRemaining -= Time.deltaTime;
                    DisplayTime(_timeRemaining);
                }
                else
                {
                    _timerIsRunning = false;
                    onTimeRunOut.Raise();
                }
            }
        }

        private void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
}