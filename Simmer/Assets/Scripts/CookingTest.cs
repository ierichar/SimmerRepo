using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;
using Simmer.FoodData;
using Simmer.Items;
using Simmer.Player;

public class CookingTest : MonoBehaviour
{
    [SerializeField] private CombineApplianceData _ovenTest;
    [SerializeField] private IngredientData _ingredientData;
    [SerializeField] LayerMask _interactableLayerMask;

    private void Awake()
    {
        FoodItem foodItem = new FoodItem(_ingredientData);

        RecipeData foundRecipe = FindRecipe(foodItem.ingredientData);
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

    private RecipeData FindRecipe(IngredientData ingredientData)
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

    private void RaycastTest()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position
            , transform.TransformDirection(Vector2.up), out hit
            , 5, _interactableLayerMask))
        {
            if (hit.transform.gameObject.TryGetComponent
                (out PlayerController playerController))
            {
                playerController.Construct();
            }
        }
    }
}
