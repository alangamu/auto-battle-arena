using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class TileVisionController : MonoBehaviour, ITileVisionController
    {
        [SerializeField]
        private GameObject _tileHidden;
        [SerializeField]
        private GameObject _tileInactive;
        [SerializeField]
        private GameObject _tileActive;

        public void SetActive()
        {
            _tileHidden.SetActive(false);
            _tileInactive.SetActive(false);
            _tileActive.SetActive(true);
        }

        public void SetInactive()
        {
            _tileHidden.SetActive(false);
            _tileInactive.SetActive(true);
            _tileActive.SetActive(false);
        }

        public void SetHidden()
        {
            _tileHidden.SetActive(true);
            _tileInactive.SetActive(false);
            _tileActive.SetActive(false);
        }
    }
}