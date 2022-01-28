using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.UI
{
    public class ImageManager : MonoBehaviour
    {
        public Image image { get; private set; }
        public RectTransform rectTransform { get; private set; }

        public void Construct()
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
    }
}