using TMPro;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapTileIndicators : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _coordinatesText;
        [SerializeField]
        private TMP_Text _elevationText;
        [SerializeField]
        private TMP_Text _moistureText;

        public void Initialize(int q, int r)
        {
            _coordinatesText.text = $"{q}, {r}";
        }

        public void SetElevation(string elevation)
        {
            _elevationText.text = elevation;
        }

        public void SetMoisture(string moisture)
        {
            _moistureText.text = moisture.ToString();
        }
    }
}