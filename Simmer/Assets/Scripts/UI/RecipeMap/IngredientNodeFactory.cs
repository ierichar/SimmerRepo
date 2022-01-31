using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class IngredientNodeFactory : MonoBehaviour
    {
        [SerializeField] IngredientNode ingredientNodePrefab;

        private List<IngredientNode> ingredientNodeList
            = new List<IngredientNode>();

        public void Construct()
        {

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

        public void ClearAllNodes()
        {
            for (int i = 0; i < ingredientNodeList.Count; ++i)
            {
                Destroy(ingredientNodeList[i].gameObject);
            }
            ingredientNodeList.Clear();
        }
    }
}