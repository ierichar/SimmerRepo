using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.FoodData
{
    [Serializable]
    public class IngredientLayer
    {
        public IngredientLayer(IngredientData ingredientData
            , Sprite layerSprite
            , int layerNum)
        {
            this.ingredientData = ingredientData;
            this.layerSprite = layerSprite;
            this.layerNum = layerNum;
        }

        public IngredientData ingredientData;
        public Sprite layerSprite;
        public int layerNum;
    }
}