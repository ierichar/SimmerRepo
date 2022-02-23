using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class TextNodeFactory : MonoBehaviour
    {
        private RectTransform _rectTransform;

        [SerializeField] TextNode textNodePrefab;

        private List<TextNode> textNodeList
            = new List<TextNode>();

        public void Construct()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetPosition(Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
        }

        public void Displace(Vector2 displacement)
        {
            _rectTransform.anchoredPosition += displacement;
        }

        public TextNode SpawnNode(
            string text
            , Vector2 position)
        {
            TextNode newNode = Instantiate(textNodePrefab, transform);

            newNode.Construct(text, position);
            textNodeList.Add(newNode);
            return newNode;
        }

        public void ClearAll()
        {
            for (int i = 0; i < textNodeList.Count; ++i)
            {
                Destroy(textNodeList[i].gameObject);
            }
            textNodeList.Clear();
        }
    }
}