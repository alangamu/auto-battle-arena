using AutoFantasy.Scripts.ScriptableObjects.Events;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField]
        private StringGameEvent sceneChangeEvent;

        private void OnEnable()
        {
            sceneChangeEvent.OnRaise += LoadScene;
        }

        private void OnDisable()
        {
            sceneChangeEvent.OnRaise -= LoadScene;
        }

        private async void LoadScene(string sceneName)
        {
            var scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;

            do
            {
                await Task.Delay(100);
            } while (scene.progress < .9f);

            await Task.Delay(1000);

            scene.allowSceneActivation = true;
        }
    }
}