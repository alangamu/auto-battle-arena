using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioEvent _audioEvent;
        [SerializeField]
        private AudioSource _audioSource;

        private void OnEnable()
        {
            _audioEvent.OnRaise += PlayAudio;
        }

        private void OnDisable()
        {
            _audioEvent.OnRaise -= PlayAudio;
        }

        private void PlayAudio(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}