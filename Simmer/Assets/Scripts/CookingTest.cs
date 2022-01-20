using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;
using Simmer.FoodData;
using Simmer.Items;

public class CookingTest : MonoBehaviour
{
    [SerializeField] private CombineApplianceData _ovenTest;
    [SerializeField] private IngredientData _ingredientData;

    private void Awake()
    {
        FoodItem foodItem = new FoodItem(_ingredientData);

        RecipeData foundRecipe = GetRecipe(foodItem.ingredientData);
        if (foundRecipe)
        {
            print("difficultyLevel: " + foundRecipe.difficultyLevel);
            print("baseActionTime: " + foundRecipe.baseActionTime);
            print("resultIngredient: " + foundRecipe.resultIngredient);
        }
        else
        {
            print("Not found");
        }
    }

    private RecipeData GetRecipe(IngredientData ingredientData)
    {
        foreach(RecipeData recipe in _ovenTest.ingredientRecipeList)
        {
            if (recipe.ingredientDataList.Contains(ingredientData))
            {
                return recipe;
            }
        }

        return null;
    }
}
