using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;
using Simmer.SceneManagement;

namespace Simmer.UI.RecipeMap
{
    public class IngredientNodeFactory : MonoBehaviour
    {
        private RectTransform _rectTransform;
        [SerializeField] private IngredientNode _ingredientNodePrefab;

        [SerializeField] private bool _isUnknownHidden;

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

        //public IngredientNode SpawnSpecialNode(
        //    SpecialNodeData nodeData
        //    , Vector2 position)
        //{

        //}

        public IngredientNode SpawnIngredientNode(
            IngredientData ingredient
            , Vector2 position)
        {
            IngredientNode newNode = Instantiate(_ingredientNodePrefab, transform);

            if(_isUnknownHidden && !GlobalPlayerData.knownIngredientList
                .Contains(ingredient))
            {
                newNode.Construct(null, position);
            }
            else
            {
                newNode.Construct(ingredient, position);
            }

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