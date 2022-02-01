using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapManager : MonoBehaviour
    {
        public IngredientNodeFactory ingredientNodeFactory { get; private set; }
        public EdgeLineFactory edgeLineFactory { get; private set; }
        public RecipeMapGenerator recipeMapGenerator { get; private set; }
        public TreeNodePositioning treeNodePositioning { get; private set; }

        public AllFoodData allFoodData;

        public void Construct()
        {
            ingredientNodeFactory = GetComponentInChildren<IngredientNodeFactory>();
            edgeLineFactory = GetComponentInChildren<EdgeLineFactory>();
            recipeMapGenerator = GetComponent<RecipeMapGenerator>();
            treeNodePositioning = GetComponent<TreeNodePositioning>();

            allFoodData.ConstructRecipeResultDict();

            ingredientNodeFactory.Construct();
            edgeLineFactory.Construct();
            treeNodePositioning.Construct(this);
            recipeMapGenerator.Construct(this);
        }

    }
}