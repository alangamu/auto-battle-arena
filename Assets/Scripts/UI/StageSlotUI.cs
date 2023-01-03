using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class StageSlotUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject stageNormal;
        [SerializeField]
        private GameObject stageSelected;
        [SerializeField]
        private GameObject stageLocked;
        [SerializeField]
        private GameObject stageCompleted;

        [SerializeField]
        private TMP_Text stageNumberText;

        [SerializeField]
        private IntVariable maxStageToLoad;
        [SerializeField]
        private IntVariable stageToLoad;

        [SerializeField]
        private StageSlotUIGameEvent activateSlot; 

        private StageSO _stage;
        private bool _isNormal;
        private bool _isLocked;
 
        public void Setup(StageSO stage)
        {
            _stage = stage;
            string stageNumberString = _stage.StageNumber.ToString();
            stageNumberText.text = stageNumberString;

            _isNormal = _stage.StageNumber <= maxStageToLoad.Value && !_stage.IsCompleted;
            _isLocked = _stage.StageNumber > maxStageToLoad.Value;

            SetSlot();
            
            if (_stage.StageNumber == stageToLoad.Value)
            {
                SetSelected();
            }
        }

        private void OnEnable()
        {
            activateSlot.OnRaise += StageSlotUIGameEvent_OnRaise;
        }

        private void OnDisable()
        {
            activateSlot.OnRaise -= StageSlotUIGameEvent_OnRaise;
        }

        private void StageSlotUIGameEvent_OnRaise(StageSlotUI stageSlotUI)
        {
            if (stageSlotUI == this)
            {
                SetSelected();
                return;
            }

            Deselect();
        }

        private void Awake()
        {
            stageNormal.SetActive(false);
            stageLocked.SetActive(false);
            stageCompleted.SetActive(false);
            stageSelected.SetActive(false);
        }

        public void SelectStage()
        {
            activateSlot.Raise(this);
        }

        private void SetSelected()
        {
            stageSelected.SetActive(true);
            stageNormal.SetActive(false);
            stageCompleted.SetActive(false);
            stageToLoad.Value = _stage.StageNumber;
        }

        private void Deselect()
        {
            stageSelected.SetActive(false);
            stageNormal.SetActive(_isNormal);
            stageCompleted.SetActive(_stage.IsCompleted);
        }

        private void SetSlot()
        {
            stageNormal.SetActive(_isNormal);
            stageLocked.SetActive(_isLocked);
            stageCompleted.SetActive(_stage.IsCompleted);
        }
    }
}