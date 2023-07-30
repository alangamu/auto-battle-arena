using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class WinGameController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _winGamePopup;
        [SerializeField]
        private MapEnemyStageSO _finalEnemyStageSO;

        private void OnEnable()
        {
            _winGamePopup.SetActive(false);
        }

        private void Start()
        {
            if (_finalEnemyStageSO.IsDefeated)
            {
                _winGamePopup.SetActive(true);
            }
        }

    }
}