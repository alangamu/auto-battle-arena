using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class TavernTest : MonoBehaviour
    {
        [SerializeField]
        private GameEvent addHeroToTavern;

        private void OnGUI()
        {
            if (GUILayout.Button("Create Privateer"))
            {
                addHeroToTavern.Raise();
            }
        }
    }
}