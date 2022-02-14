using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Simmer.FoodData;
using Simmer.Appliance;

namespace Simmer.Editor
{
    public class ValidateIngredientData
    {
        [MenuItem("Tools/Validate FoodData/IngredientData %#.")]
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
                    data.applianceRecipeListDict.Count == 0)
                {
                    Debug.LogError("IngredientData \"" + data.name
                        + "\" has false isFinalProduct so " +
                        "cannot have an empty applianceRecipeDict");
                    isValidated = false;
                }

                isValidated = TestApplianceRecipeListDict(data);

                isValidated = TestIngredientLayerDict(data);
            }

            Debug.Log(isValidated + " ValidateIngredientData");
            return isValidated;
        }

        private static bool TestApplianceRecipeListDict(IngredientData data)
        {
            bool isValidated = true;
            foreach (var pair in data
                    .applianceRecipeListDict)
            {
                // Test applianceRecipeDict of key SoloApplianceData
                // has only 1 RecipeData
                if (pair.Key.GetType() == typeof(SoloApplianceData))
                {
                    if (pair.Value.Count > 1)
                    {
                        Debug.LogError("IngredientData \""
                            + data.name + "\" applianceRecipeDict appliance \""
                            + pair.Key.name + "\" is a SoloApplianceData and " +
                            " cannot contain more than 1 RecipeData");
                        isValidated = false;
                    }
                }

                foreach (var recipeData in pair.Value)
                {
                    // Test applianceRecipeDict recipe missing ingredient 
                    if (!recipeData.ingredientDataList.Contains(data)
                        && !recipeData.expandedIngredientList.Contains(data))
                    {
                        Debug.LogError("IngredientData \""
                            + data.name + "\" on RecipeData \""
                            + recipeData.name + "\" is missing");
                        isValidated = false;
                    }
                }
            }

            return isValidated;
        }

        private static bool TestIngredientLayerDict(IngredientData data)
        {
            bool isValidated = true;

            List<int> indexList = new List<int>();
            foreach(var pair in data.expandLayerDict)
            {
                IngredientData ingredientData = pair.Key;
                IngredientLayer layer = pair.Value;
                int layerNum = layer.layerNum;

                if (ingredientData == null)
                {
                    Debug.LogError("IngredientData \""
                        + data.name + "\" ingredientLayerDict ingredientData"
                        + " cannot be null");
                    isValidated = false;
                    continue;
                }

                if (layer == null)
                {
                    Debug.LogError("IngredientData \""
                        + data.name + "\" ingredientLayerDict ingredientLayer"
                        + " cannot be null");
                    isValidated = false;
                }

                if (layer.ingredientData == null)
                {
                    Debug.LogError("IngredientData \""
                        + data.name + "\" ingredientLayerDict" +
                        " ingredientLayer.ingredientData cannot be null");
                    isValidated = false;
                }

                if (indexList.Contains(layerNum))
                {
                    Debug.LogError("IngredientData \""
                        + data.name + "\" ingredientLayerDict" +
                        " ingredientLayer.layerNum cannot contain" +
                        " duplicate number \"" + layerNum + "\"");
                    isValidated = false;
                }
                indexList.Add(layerNum);
            }

            return isValidated;
        }
    }
}