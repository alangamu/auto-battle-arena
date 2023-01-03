using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class StageSlotUISpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform parentTransform;
        [SerializeField]
        private GameObject stageSlotUIPrefab;
        [SerializeField]
        private DatabaseStage stages;

        private void OnEnable()
        {
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }

            foreach (var stage in stages.Items)
            {
                GameObject stageSlotUIObject = Instantiate(stageSlotUIPrefab, parentTransform);
                if (stageSlotUIObject.TryGetComponent(out StageSlotUI stageSlotUI))
                {
                    //TODO make interface (SOLID)
                    stageSlotUI.Setup(stage);
                }
            }
        }
    }
}