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

        public List<RecipeData> recipeEdgeList
            = new List<RecipeData>();

        public bool ContainsValidRecipe(ApplianceData applianceData)
        {
            foreach(RecipeData recipeData in recipeEdgeList)
            {
                if (recipeData.appliance == applianceData)
                {
                    return true;
                }
            }

            return false;
        }
    }
}