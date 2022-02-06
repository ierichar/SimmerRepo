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

        [SerializeField] List<RecipeData> _recipeEdgeList
            = new List<RecipeData>();

        public Dictionary<ApplianceData, List<RecipeData>>
            applianceRecipeListDict
            = new Dictionary<ApplianceData, List<RecipeData>>();

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
        }

    }
}