using System;
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

        Dictionary<string, IngredientData> ingredientNameDictionary
            = new Dictionary<string, IngredientData>();

        public List<IngredientData> rawIngredientList
            = new List<IngredientData>();

        public List<IngredientData> finalIngredientList
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

            if(recipeResultDict.Count == 0)
            {
                Debug.LogError("ConstructRecipeResultDict failed: empty");
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

            ConstructIngredientNameDictionary();

            ConstructRecipeResultDict();

            ConstructFilteredIngredientList(
                ref rawIngredientList, RawPredicate);

            ConstructFilteredIngredientList(
                ref finalIngredientList, FinalPredicate);
        }

        private void ConstructFilteredIngredientList(
            ref List<IngredientData> result, Predicate<IngredientData> predicate)
        {
            result = allIngredientDataList.FindAll(predicate);
        }

        private bool RawPredicate(IngredientData item)
        {
            // If the item isn't a result in any recipe
            // it's a raw ingredient
            return !recipeResultDict.ContainsKey(item);
        }

        private bool FinalPredicate(IngredientData item)
        {
            return item.isFinalProduct;
        }

        private void ConstructIngredientNameDictionary()
        {
            ingredientNameDictionary.Clear();

            foreach (IngredientData ingredient in allIngredientDataList)
            {
                ingredientNameDictionary.Add(ingredient.name, ingredient);
            }
        }
    }
}