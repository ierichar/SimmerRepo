using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.FoodData
{
    [CreateAssetMenu(fileName = "New AllFoodData"
        , menuName = "FoodData/AllFoodData")]

    public class AllFoodData : ScriptableObject
    {
        public Dictionary<IngredientData, RecipeData> recipeResultDict
            = new Dictionary<IngredientData, RecipeData>();

        public List<IngredientData> allIngredientDataList
            = new List<IngredientData>();

        public List<RecipeData> allRecipeDataList
            = new List<RecipeData>();

        public void ConstructRecipeResultDict()
        {
            recipeResultDict.Clear();
            foreach (RecipeData recipe in allRecipeDataList)
            {
                recipeResultDict.Add(recipe.resultIngredient, recipe);
            }
        }

        public void Construct(
            IngredientData[] ingredientDataArray
            , RecipeData[] recipeDataArray)
        {
            allIngredientDataList.Clear();
            foreach (IngredientData ingredient in ingredientDataArray)
            {
                allIngredientDataList.Add(ingredient);
            }

            allRecipeDataList.Clear();
            foreach (RecipeData recipe in recipeDataArray)
            {
                allRecipeDataList.Add(recipe);
            }
        }
    }
}