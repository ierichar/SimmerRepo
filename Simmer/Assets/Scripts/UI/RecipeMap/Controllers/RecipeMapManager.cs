using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;
using Simmer.Items;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapManager : MonoBehaviour
    {
        public ItemSlotManager apexRecipeSlot;

        public IngredientNodeFactory ingredientNodeFactory { get; private set; }
        public TextNodeFactory textNodeFactory { get; private set; }
        public EdgeLineFactory edgeLineFactory { get; private set; }
        public RecipeMapGenerator recipeMapGenerator { get; private set; }
        public TreeNodePositioning treeNodePositioning { get; private set; }
        public RecipeMapZoom recipeMapZoom { get; private set; }


        public AllFoodData allFoodData;

        public void Construct()
        {
            apexRecipeSlot.Construct(0);

            ingredientNodeFactory = GetComponentInChildren<IngredientNodeFactory>();
            textNodeFactory = GetComponentInChildren<TextNodeFactory>();
            edgeLineFactory = GetComponentInChildren<EdgeLineFactory>();
            recipeMapGenerator = GetComponent<RecipeMapGenerator>();
            treeNodePositioning = GetComponent<TreeNodePositioning>();
            recipeMapZoom = GetComponent<RecipeMapZoom>();

            allFoodData.ConstructRecipeResultDict();

            ingredientNodeFactory.Construct();
            textNodeFactory.Construct();
            edgeLineFactory.Construct();
            treeNodePositioning.Construct(this);
            recipeMapGenerator.Construct(this);
            recipeMapZoom.Construct();
        }
    }
}