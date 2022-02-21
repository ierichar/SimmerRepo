using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapEventManager : MonoBehaviour
    {
        public UnityEvent<IngredientData> OnShowRecipeMap
            = new UnityEvent<IngredientData>();

        public UnityEvent<IngredientData> OnShowUtilityMap
            = new UnityEvent<IngredientData>();

        public void Construct()
        {

        }
    }
}