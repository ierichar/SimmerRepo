using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapEventManager : MonoBehaviour
    {
        public UnityEvent<IngredientData> OnUpdateMap
            = new UnityEvent<IngredientData>();

        public void Construct()
        {

        }
    }
}