using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;
using Simmer.Items;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapManager : MonoBehaviour, IControlUI
    {
        private RecipeMapWindow _recipeMapWindow;

        public RecipeMapEventManager recipeMapEventManager { get; private set; }
        public IngredientNodeFactory ingredientNodeFactory { get; private set; }
        public TextNodeFactory textNodeFactory { get; private set; }
        public EdgeLineFactory edgeLineFactory { get; private set; }
        public RecipeMapGenerator recipeMapGenerator { get; private set; }
        public TreeNodePositioning treeNodePositioning { get; private set; }
        public RecipeMapZoom recipeMapZoom { get; private set; }

        public AllFoodData allFoodData;

        public void Construct()
        {
            _recipeMapWindow = FindObjectOfType<RecipeMapWindow>(true);

            recipeMapEventManager = GetComponent<RecipeMapEventManager>();
            ingredientNodeFactory = GetComponentInChildren<IngredientNodeFactory>();
            textNodeFactory = GetComponentInChildren<TextNodeFactory>();
            edgeLineFactory = GetComponentInChildren<EdgeLineFactory>();
            recipeMapGenerator = GetComponent<RecipeMapGenerator>();
            treeNodePositioning = GetComponent<TreeNodePositioning>();
            recipeMapZoom = GetComponent<RecipeMapZoom>();

            allFoodData.ConstructRecipeResultDict();

            recipeMapEventManager.Construct();
            ingredientNodeFactory.Construct();
            textNodeFactory.Construct();
            edgeLineFactory.Construct();
            treeNodePositioning.Construct(this);
            recipeMapGenerator.Construct(this);
            recipeMapZoom.Construct();
        }

        public void ToggleActive()
        {
            _recipeMapWindow.ToggleActive();
        }
    }
}