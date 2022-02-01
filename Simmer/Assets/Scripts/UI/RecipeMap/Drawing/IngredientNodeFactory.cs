using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class IngredientNodeFactory : MonoBehaviour
    {
        private RectTransform _rectTransform;

        [SerializeField] IngredientNode ingredientNodePrefab;

        private List<IngredientNode> ingredientNodeList
            = new List<IngredientNode>();

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

        public IngredientNode SpawnIngredientNode(
            IngredientData ingredient
            , Vector2 position)
        {
            IngredientNode newNode = Instantiate(ingredientNodePrefab, transform);

            newNode.Construct(ingredient, position);
            ingredientNodeList.Add(newNode);
            return newNode;
        }

        public void ClearAll()
        {
            for (int i = 0; i < ingredientNodeList.Count; ++i)
            {
                Destroy(ingredientNodeList[i].gameObject);
            }
            ingredientNodeList.Clear();
        }
    }
}