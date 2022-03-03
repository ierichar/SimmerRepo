using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace Simmer.UI
{
    public class CanvasGroupManager : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        public void Construct()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public Tween Fade(float newAlpha, float duration, Ease ease)
        {
            return _canvasGroup.DOFade(newAlpha, duration)
                .SetEase(ease);
        }
    }
}