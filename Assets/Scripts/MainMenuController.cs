using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _settingsPanel;
        [SerializeField]
        private StringGameEvent _sceneChangeEvent;
        [SerializeField]
        private string _sceneName;
        [SerializeField]
        private BoolVariable _isNewGame;

        public void ContinuePlay()
        {
            _isNewGame.SetValue(false);
        }

        public void NewGame()
        {
            _isNewGame.SetValue(true);
            _sceneChangeEvent.Raise(_sceneName);
        }

        public void LoadGame()
        {

        }

        public void ShowSettings()
        {
            _settingsPanel.SetActive(true);
        }

        public void QuitGame() 
        {
            Application.Quit();
        }

        private void Start()
        {
            _settingsPanel.SetActive(false);
        }
    }
}