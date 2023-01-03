using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoFantasy.Scripts
{
    public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] 
        private GameEvent refreshUI;

        private RectTransform _rectTransform;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _rectTransform = transform as RectTransform;
            _canvas = FindObjectOfType<Canvas>();
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