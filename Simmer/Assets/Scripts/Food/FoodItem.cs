using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.Items
{
    public class FoodItem
    {
        public IngredientData ingredientData { get; private set; }
        public int _currentQuality { get; private set; }

        public FoodItem(IngredientData ingredientData)
        {
            this.ingredientData = ingredientData;
            _currentQuality = 1;
        }
        public FoodItem(IngredientData ingredientData, int quality)
        {
            this.ingredientData = ingredientData;
            _currentQuality = quality; 
        }

        public void SetQuality(int toSet)
        {
            _currentQuality = toSet;
        }
    }
}