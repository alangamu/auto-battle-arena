using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using System.Collections;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class HeroStatsUIController : MonoBehaviour
    {
        [SerializeField]
        private Transform _gridTransform;
        [SerializeField]
        private GameObject _heroStatUIPrefab;
        [SerializeField]
        private GameObject _heroDetailsPopup;
        [SerializeField]
        private GameEvent _showHeroDetails;

        private HeroStatSO[] _stats;


        private void OnEnable()
        {
            _heroDetailsPopup.SetActive(false);
            _stats = Resources.LoadAll<HeroStatSO>("Stats");
            _showHeroDetails.OnRaise += ShowPopup;
            
            foreach (Transform item in _gridTransform)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in _stats)
            {
                GameObject heroStatGO = Instantiate(_heroStatUIPrefab, _gridTransform);
            
                if (heroStatGO.TryGetComponent(out HeroStatUI heroStatUI))
                {
                    heroStatUI.Initialize(item);
                }
            }
        }

        private void OnDisable()
        {
            _showHeroDetails.OnRaise -= ShowPopup;
        }

        private void ShowPopup()
        {

        }
    }
}