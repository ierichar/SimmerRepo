using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

using Simmer.FoodData;

namespace Simmer.Editor
{
    public class AllFoodDataFactory
    {
        [MenuItem("Tools/Update AllFoodData %#p")]
        private static void NewMenuOption()
        {
            bool testsPassed = true;
            bool ingredientTest = ValidateIngredientData.Validate();
            bool recipeTest = ValidateRecipeData.Validate();

            testsPassed = ingredientTest & recipeTest;

            if(testsPassed)
            {
                SpawnNewFoodData();
            }
            else
            {
                Debug.LogError("AllFoodDataFactory tests failed");
            }
        }

        public static void SpawnNewFoodData()
        {
            Debug.Log("Begin Update AllFoodData ----------------------------");

            AllFoodData[] allFoodDataArray = EditorSimmerUtil
                .GetAllInstances<AllFoodData>();

            if(allFoodDataArray.Length != 1)
            {
                Debug.LogError("There should be only 1 instance of AllFoodData");
                return;
            }

            AllFoodData allFoodData = allFoodDataArray[0];

            IngredientData[] ingredientArray = EditorSimmerUtil
                .GetAllInstances<IngredientData>();

            RecipeData[] recipeArray = EditorSimmerUtil
                .GetAllInstances<RecipeData>();

            allFoodData.Construct(ingredientArray, recipeArray);
            EditorSimmerUtil.SaveAsset(allFoodData);

            Debug.Log("End Update AllFoodData ----------------------------");
        }
    }
}