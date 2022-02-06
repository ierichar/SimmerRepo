using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

using Simmer.FoodData;

namespace Simmer.Editor
{
    public class ValidateRecipeData : MonoBehaviour
    {
        [MenuItem("Tools/Validate FoodData/RecipeData %#o")]
        private static void NewMenuOption()
        {
            Validate();
        }

        public static bool Validate()
        {
            Debug.Log("Begin ValidateRecipeData ----------------------------");
            bool isValidated = true;

            RecipeData[] dataArray = EditorSimmerUtil
                .GetAllInstances<RecipeData>();

            foreach (RecipeData recipe in dataArray)
            {
                Type recipeType = typeof(RecipeData);
                FieldInfo[] recipeInfo = recipeType
                    .GetFields(BindingFlags.Instance | BindingFlags.Public);

                foreach (FieldInfo fieldInfo in recipeInfo)
                {
                    object thisFieldVal = fieldInfo.GetValue(recipe);

                    if (thisFieldVal == null)
                    {
                        Debug.LogError("Field \""
                            + fieldInfo.Name + "\" cannot be null on \""
                            + recipe.name + "\"");
                        isValidated = false;
                    }
                }

                if (recipe.ingredientDataList.Count == 0)
                {
                    Debug.LogError("RecipeData \"" + recipe.name
                        + "\" cannot have an empty ingredientDataList");
                    isValidated = false;
                }

                foreach (IngredientData ingredient in recipe.ingredientDataList)
                {
                    if (!ingredient.applianceRecipeListDict
                        .ContainsKey(recipe.applianceData))
                    {
                        Debug.LogError("RecipeData \"" + recipe
                            + "\" of ApplianceData " + recipe.applianceData
                            + " contains ingredient \"" + ingredient
                            + "\" with missing ApplianceData");
                        isValidated = false;
                    }
                }
            }

            Debug.Log(isValidated + " ValidateRecipeData");
            return isValidated;
        }
    }
}