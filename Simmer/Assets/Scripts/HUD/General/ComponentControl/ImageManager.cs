using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace Simmer.UI
{
    public class ImageManager : MonoBehaviour
    {
        public Image image { get; private set; }
        public RectTransform rectTransform { get; private set; }

        public virtual void Construct()
        {
            image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            image.color = color;
        }

        public Tween Fade(float newAlpha, float duration, Ease ease)
        {
            return image.DOFade(newAlpha, duration)
                .SetEase(ease);
        }

        public void SetActive(bool isActive)
        {
            image.enabled = isActive;
        }
    }
}