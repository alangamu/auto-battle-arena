using System.Collections;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.FXsTypes
{
    public class FXAtAttacker : FXsTypeSO
    {
        [SerializeField]
        private GameObject _fxPrefab;
        public override void PerformEffect(Transform attacker, Transform target, float movementDuration)
        {
            
        }
    }
}