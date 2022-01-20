using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;

namespace Simmer.FoodData
{
    [CreateAssetMenu(fileName = "New IngredientData"
        , menuName = "FoodData/IngredientData")]

    public class IngredientData : ScriptableObject
    {
        public Sprite sprite;
        public int baseValue;

        public List<RecipeData> ingredientLinkList
            = new List<RecipeData>();
    }
}