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

        [Serializable]
        public class IngredientLayer
        {
            public IngredientData ingredientData;
            public int layerNum;
        }
        public List<IngredientLayer> ingredientLayerList
            = new List<IngredientLayer>();

        public Dictionary<IngredientData, int>
            ingredientLayerDict { get; private set; }

        public int maxPerRecipe;

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

            ingredientLayerDict = new Dictionary<IngredientData, int>();
            foreach (IngredientLayer item in ingredientLayerList)
            {
                ingredientLayerDict.Add(item.ingredientData, item.layerNum);
            }
        }

    }
}