using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapManager : MonoBehaviour
    {
        public IngredientNodeFactory ingredientNodeFactory { get; private set; }
        public RecipeMapGenerator recipeMapGenerator { get; private set; }

        private void Awake()
        {
            ingredientNodeFactory = GetComponent<IngredientNodeFactory>();
            recipeMapGenerator = GetComponent<RecipeMapGenerator>();

            ingredientNodeFactory.Construct();
            recipeMapGenerator.Construct(this);
        }

    }
}