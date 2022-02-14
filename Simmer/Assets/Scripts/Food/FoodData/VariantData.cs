using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.FoodData
{
    [Serializable]
    public class VariantData
    {
        public VariantData(List<IngredientData> childIngredientList
            , int minCount
            , int maxCount)
        {
            this.childIngredientList = childIngredientList;
            this.minCount = minCount;
            this.maxCount = maxCount;
        }

        public List<IngredientData> childIngredientList;
        public int minCount;
        public int maxCount;
    }
}