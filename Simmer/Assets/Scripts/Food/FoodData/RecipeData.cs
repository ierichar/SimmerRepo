using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;

namespace Simmer.FoodData
{
    [CreateAssetMenu(fileName = "New RecipeData"
        , menuName = "FoodData/RecipeData")]

    public class RecipeData : ScriptableObject
    {
        public List<IngredientData> ingredientDataList
            = new List<IngredientData>();

        public ApplianceData applianceData;
        public int difficultyLevel;
        public float baseActionTime;
        public IngredientData resultIngredient;
    }
}