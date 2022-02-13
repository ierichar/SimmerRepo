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

        public readonly List<IngredientData> expandedIngredientList
            = new List<IngredientData>();

        public ApplianceData applianceData;
        public int difficultyLevel;
        public float baseActionTime;
        public IngredientData resultIngredient;

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
            expandedIngredientList.Clear();
            foreach (IngredientData ingredientData in ingredientDataList)
            {
                RecursiveExpandedIngredientList(ingredientData);
            }
        }

        private void RecursiveExpandedIngredientList(IngredientData parent)
        {
            if (parent.ingredientLayerList.Count == 0)
            {
                expandedIngredientList.Add(parent);
            }

            foreach (IngredientLayer childLayer in parent.ingredientLayerList)
            {
                RecursiveExpandedIngredientList(childLayer.ingredientData);
            }
        }
    }
}