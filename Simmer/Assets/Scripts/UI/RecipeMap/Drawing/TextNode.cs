using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class TextNode : MonoBehaviour
    {
        private UITextManager _textManager;
        private RectTransform _rectTransform;

        public string text { get; private set; }

        public void Construct(string text, Vector2 position)
        {
            this.text = text;

            _textManager = GetComponent<UITextManager>();
            _textManager.Construct();

            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = position;

            _textManager.SetText(text);
        }
    }
}