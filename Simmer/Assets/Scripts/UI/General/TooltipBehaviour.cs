using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI
{
    public class TooltipBehaviour : MonoBehaviour
    {
        private UITextManager _textManager;
        private ImageManager _imageManager;
        private RectTransform _rectTransform;
        private Transform _fallbackTransform;

        [SerializeField] private float _textPadding;

        private ITooltipable currentTarget;

        public void Construct(Transform fallbackTransform)
        {
            _fallbackTransform = fallbackTransform;

            _textManager = GetComponentInChildren<UITextManager>();
            _textManager.Construct();

            _imageManager = GetComponentInChildren<ImageManager>();
            _imageManager.Construct();

            _rectTransform = GetComponent<RectTransform>();

            SetVisible(false);
        }

        public void ShowTooltip(string text, bool isVisible)
        {
            if (isVisible)
            {
                SetVisible(true);
                SetText(text);
            }
            else
            {
                SetText("");
                SetVisible(false);
            }
        }

        public void ShowTooltip(string text)
        {
            ShowTooltip(text, true);
        }

        public void SetTarget(Transform newParent
            , ITooltipable target
            , bool isTarget)
        {
            if (isTarget)
            {
                transform.SetParent(newParent);
                currentTarget = target;
                _rectTransform.anchoredPosition = Vector2.zero;
            }
            else
            {
                transform.SetParent(_fallbackTransform);
                _rectTransform.anchoredPosition = Vector2.zero;
                currentTarget = null;
            }
            
        }

        private void SetText(string text)
        {
            _textManager.SetText(text);

            Vector2 backgroundSize = new Vector2(
                _textManager.textMeshPro.preferredWidth + _textPadding * 2f
                , _textManager.textMeshPro.preferredHeight + _textPadding * 2f);

            _imageManager.rectTransform.sizeDelta = backgroundSize;
        }

        private void SetVisible(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}