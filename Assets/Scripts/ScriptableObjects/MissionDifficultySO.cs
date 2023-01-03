using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Mission/MissionDifficultySO")]
    public class MissionDifficultySO : ScriptableObject
    {
        [Range(0f,1f)]
        public float DifficultyMultiplier;
    }
}