using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoFantasy.Scripts
{
    public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] 
        private GameEvent refreshUI;

        private Canvas _canvas;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
            _rectTransform = transform as RectTransform;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = .6f;
            _canvasGroup.blocksRaycasts = false;

            transform.SetParent(_canvas.transform);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            refreshUI.Raise();
            Destroy(gameObject);
        }
    }
}