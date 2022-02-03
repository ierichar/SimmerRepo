using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

using Simmer.FoodData;

namespace Simmer.Editor
{
    public class ValidateIngredientData
    {
        [MenuItem("Tools/Validate FoodData/IngredientData %#i")]
        private static void NewNestedMenuOption()
        {
            Validate();
        }

        public static bool Validate()
        {
            Debug.Log("Begin ValidateFoodData ----------------------------");
            bool isValidated = true;

            IngredientData[] dataArray = EditorSimmerUtil
                .GetAllInstances<IngredientData>();

            foreach (IngredientData data in dataArray)
            {
                Type dataType = typeof(IngredientData);
                FieldInfo[] ingredientInfo = dataType
                    .GetFields(BindingFlags.Instance | BindingFlags.Public);

                // Test null fields
                foreach (FieldInfo fieldInfo in ingredientInfo)
                {
                    object thisFieldVal = fieldInfo.GetValue(data);

                    if (thisFieldVal == null)
                    {
                        Debug.LogError("Field \""
                            + fieldInfo.Name + "\" cannot be null on \""
                            + data.name + "\"");
                        isValidated = false;
                    }
                }

                // Test empty applianceRecipeDict
                if (!data.isFinalProduct &&
                    data.applianceRecipeDict.Count == 0)
                {
                    Debug.LogError("IngredientData \"" + data.name
                        + "\" has false isFinalProduct so " +
                        "cannot have an empty applianceRecipeDict");
                    isValidated = false;
                }

                // Test applianceRecipeDict recipe missing ingredient 
                foreach (RecipeData recipeData in data
                    .applianceRecipeDict.Values)
                {
                    if (!recipeData.ingredientDataList.Contains(data))
                    {
                        Debug.LogError("IngredientData \""
                            + data.name + "\" on RecipeData \""
                            + recipeData.name + "\" is missing");
                        isValidated = false;
                    }
                }
            }

            Debug.Log(isValidated + " ValidateIngredientData");
            return isValidated;
        }
    }
}