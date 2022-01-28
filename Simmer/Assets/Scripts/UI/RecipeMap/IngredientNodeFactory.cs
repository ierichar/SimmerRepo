using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class IngredientNodeFactory : MonoBehaviour
    {
        [SerializeField] IngredientNode ingredientNodePrefab;

        public void Construct()
        {

        }

        public IngredientNode SpawnIngredientNode(
            IngredientData ingredient
            , Vector2 position)
        {
            IngredientNode newNode = Instantiate(ingredientNodePrefab, transform);

            newNode.Construct(ingredient, position);

            return newNode;
        }
    }
}