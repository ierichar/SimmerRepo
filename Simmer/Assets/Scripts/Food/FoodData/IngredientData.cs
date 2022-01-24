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

        public Dictionary<ApplianceData, RecipeData> applianceRecipeDict
            = new Dictionary<ApplianceData, RecipeData>();

        private void OnValidate()
        {
            Construct();
        }

        private void Construct()
        {
            applianceRecipeDict.Clear();
            foreach (RecipeData recipe in _recipeEdgeList)
            {
                if(recipe != null && recipe.applianceData)
                {
                    applianceRecipeDict.Add(recipe.applianceData, recipe);
                } 
            }
        }

    }
}