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
        private Camera _mainCamera;
        private ImageManager _backgroundImageManager;
        private RectTransform _rectTransform;
        private LayoutElement _layoutElement;
        private CanvasGroup _canvasGroup;

        [SerializeField] private int _characterWrapLimit;
        [SerializeField] private float _positionOffsetX;
        [SerializeField] private float _positionOffsetY;
        [SerializeField] private float _showDelay;

        [SerializeField] private float _scaleTweenDuration;
        [SerializeField] private Ease _scaleTweenEase;

        private Coroutine currentDelay = null;

        private void Awake()
        {
            if (instance != null) Destroy(instance.gameObject);
            instance = this;

            _mainCamera = Camera.main;

            _layoutElement = GetComponentInChildren<LayoutElement>();

            _headerTextManager.Construct();
            _bodyTextManager.Construct();

            _backgroundImageManager = GetComponentInChildren<ImageManager>();
            _backgroundImageManager.Construct();

            _canvasGroup = GetComponent<CanvasGroup>();

            _rectTransform = GetComponent<RectTransform>();

            Hide();
        }

        public void Show(RectTransform rectTransform
            , string bodyText
            , string headerText = "")
        {
            if (currentDelay != null) return;
            currentDelay = StartCoroutine(Delay(() =>
            {
                SetText(bodyText, headerText);
                UpdatePosition(rectTransform);
                _backgroundImageManager.gameObject.SetActive(true);
                _backgroundImageManager.rectTransform.localScale
                    = Vector3.zero;
                _backgroundImageManager.rectTransform
                    .DOScale(1, _scaleTweenDuration)
                    .SetEase(_scaleTweenEase);
            }));
        }

        public void Hide()
        {
            if(currentDelay != null)
            {
                StopCoroutine(currentDelay);
                currentDelay = null;
            }
            _backgroundImageManager.rectTransform
                    .DOScale(0, _scaleTweenDuration)
                    .SetEase(_scaleTweenEase);
            //_backgroundImageManager.gameObject.SetActive(false);
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

        private void UpdatePosition(RectTransform rectTransform)
        {
            RectTransform tooltipTransform = _backgroundImageManager.rectTransform;
            //_rectTransform.sizeDelta = _backgroundImageManager
            //    .rectTransform.sizeDelta;
            //+new Vector2(_positionOffsetX, _positionOffsetY);

            //Vector2 targetPosition = Input.mousePosition;

            Rect screenRect = RectTransformToScreenSpace(rectTransform);
            Vector2 targetPosition = new Vector2(
                screenRect.x
                , screenRect.y);

            //print("x: " + screenRect.x + " y: " + screenRect.y
            //    + " width: " + screenRect.width + " height: " + screenRect.height);

            float pivotX = targetPosition.x / Screen.width;
            float pivotY = targetPosition.y / Screen.height;

            tooltipTransform.pivot = new Vector2(pivotX, pivotY);
            tooltipTransform.anchoredPosition = targetPosition;

            float thisXOffset;
            float thisYOffset;

            if (pivotX < 0.5) thisXOffset = -(screenRect.width / 2);
            else thisXOffset = (screenRect.width / 2);

            if (pivotY < 0.5) thisYOffset = -(screenRect.height / 2);
            else thisYOffset = (screenRect.height / 2);

            //tooltipTransform.anchoredPosition += new Vector2(
            //    thisXOffset, thisYOffset);


            //tooltipTransform.anchoredPosition += new Vector2(
            //    _positionOffsetX, _positionOffsetY);
        }

        private IEnumerator Delay(Action action)
        {
            yield return new WaitForSeconds(_showDelay);
            action();
        }

        private Rect RectTransformToScreenSpace(RectTransform transform)
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            return new Rect((Vector2)transform.position - (size * transform.pivot), size);
        }
    }
}