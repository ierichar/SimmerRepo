using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace Simmer.UI.Tooltips
{
    public class TooltipBehaviour : MonoBehaviour
    {
        public static TooltipBehaviour instance;

        [SerializeField] private UITextManager _headerTextManager;
        [SerializeField] private UITextManager _bodyTextManager;
        private ImageManager _backgroundImageManager;
        private RectTransform _rectTransform;
        private LayoutElement _layoutElement;
        private CanvasGroup _canvasGroup;

        [SerializeField] private int _characterWrapLimit;
        [SerializeField] private float _positionOffsetX;
        [SerializeField] private float _positionOffsetY;
        [SerializeField] private float _showDelay;

        private void Awake()
        {
            if (instance != null) Destroy(instance.gameObject);
            instance = this;

            _layoutElement = GetComponentInChildren<LayoutElement>();

            _headerTextManager.Construct();
            _bodyTextManager.Construct();

            _backgroundImageManager = GetComponentInChildren<ImageManager>();
            _backgroundImageManager.Construct();

            _canvasGroup = GetComponent<CanvasGroup>();

            _rectTransform = GetComponent<RectTransform>();

            Hide();
        }

        private void Update()
        {
            UpdatePosition();
        }

        public void Show(string bodyText, string headerText = "")
        {
            SetText(bodyText, headerText);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetText(string bodyText, string headerText = "")
        {
            bool isShowHeader = string.IsNullOrEmpty(headerText);
            _headerTextManager.gameObject.SetActive(isShowHeader);

            _headerTextManager.SetText(headerText);
            _bodyTextManager.SetText(bodyText);

            UpdateLayout();
        }

        private void UpdateLayout()
        {
            int headerLength = _headerTextManager.textMeshPro.text.Length;
            int bodyLength = _headerTextManager.textMeshPro.text.Length;

            _layoutElement.enabled = (headerLength < bodyLength
                || bodyLength > _characterWrapLimit) ? true : false;
        }

        private void UpdatePosition()
        {
            _rectTransform.sizeDelta = _backgroundImageManager
                .rectTransform.sizeDelta
                + new Vector2(_positionOffsetX, _positionOffsetY);

            Vector2 mousePosition = Input.mousePosition;

            float pivotX = Mathf.Round(mousePosition.x / Screen.width);
            float pivotY = Mathf.Round(mousePosition.y / Screen.height);

            _rectTransform.pivot = new Vector2(pivotX, pivotY);
            _rectTransform.anchoredPosition = mousePosition;

            float thisXOffset;
            float thisYOffset;

            if (pivotX < 0.5) thisXOffset = -(_positionOffsetX);
            else thisXOffset = _positionOffsetX;

            if (pivotY < 0.5) thisYOffset = -(_positionOffsetY);
            else thisYOffset = _positionOffsetY;

            //_rectTransform.anchoredPosition += new Vector2(
            //    thisXOffset, thisYOffset);
        }

        private IEnumerator Delay(Action action)
        {
            yield return new WaitForSeconds(_showDelay);
            action();
        }
    }
}