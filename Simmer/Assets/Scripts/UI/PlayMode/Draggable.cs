using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Simmer.UI
{
    public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Canvas _playCanvas;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        public void Construct(Canvas playCanvas)
        {
            _playCanvas = playCanvas;
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 0.5f;
            _canvasGroup.blocksRaycasts = false;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition
                += eventData.delta / _playCanvas.scaleFactor;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {

        }
    }
}