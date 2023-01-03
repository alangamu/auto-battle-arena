using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class TeamUISpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform parentTransform;
        [SerializeField]
        private TeamsSO teams;
        [SerializeField]
        private TeamUI teamUIPrefab;

        private void OnEnable()
        {
            Invoke(nameof(RefresUI), 0.05f);
        }

        private void RefresUI()
        {
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < teams.Teams.Count; i++)
            {
                var teamObject = Instantiate(teamUIPrefab, parentTransform);
                teamObject.Setup(i);
            }
        }
    }
}