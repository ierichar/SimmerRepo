using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;

namespace Simmer.FoodData
{
    [CreateAssetMenu(fileName = "New IngredientData"
        , menuName = "FoodData/IngredientData")]

    public class IngredientData : ScriptableObject
    {
        public Sprite sprite;
        public int baseValue;
        public bool isFinalProduct;

        [SerializeField]
        private List<RecipeData> _recipeEdgeList
            = new List<RecipeData>();

        public Dictionary<ApplianceData, List<RecipeData>>
            applianceRecipeListDict
            = new Dictionary<ApplianceData, List<RecipeData>>();

        public enum CombineMode
        {
            Additive,
            BaseOnly
        }
        public CombineMode combineMode;

        public List<IngredientLayer> ingredientLayerList
            = new List<IngredientLayer>();

        public Dictionary<IngredientData, IngredientLayer>
            ingredientLayerDict = new Dictionary<IngredientData, IngredientLayer>();

        public int maxPerRecipe;

        private string layerDictDebug = "";

        private void OnValidate()
        {
            Construct();
        }

        private void Awake()
        {
            Construct();
        }

        private void Construct()
        {
            layerDictDebug = "";

            applianceRecipeListDict.Clear();
            foreach (RecipeData recipe in _recipeEdgeList)
            {
                ApplianceData thisAppliance = recipe.applianceData;
                if (recipe != null && recipe.applianceData)
                {
                    if (applianceRecipeListDict.ContainsKey(thisAppliance))
                    {
                        applianceRecipeListDict[thisAppliance].Add(recipe);
                    }
                    else
                    {
                        List<RecipeData> applianceRecipeList = new List<RecipeData>();
                        applianceRecipeList.Add(recipe);
                        applianceRecipeListDict.Add(recipe.applianceData, applianceRecipeList);
                    }
                }
            }

            ingredientLayerDict.Clear();

            IngredientLayer baseIngredientLayer
                = new IngredientLayer(this, null, 0);
            ingredientLayerDict.Add(this, baseIngredientLayer);

            foreach (IngredientLayer item in ingredientLayerList)
            {
                RecursivePopulateLayerList(item);
            }

            //Debug.Log(this + " " + layerDictDebug);
        }

        private void RecursivePopulateLayerList(IngredientLayer ingredientLayer)
        {
            if(ingredientLayer.ingredientData.ingredientLayerList.Count == 0)
            {
                ingredientLayerDict.Add(ingredientLayer.ingredientData
                , ingredientLayer);

                layerDictDebug += ingredientLayer.ingredientData.name + " ";
            }

            foreach (IngredientLayer childLayer in ingredientLayer
                .ingredientData.ingredientLayerList)
            {
                RecursivePopulateLayerList(childLayer);
            }

        }

    }
}