using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.Items
{
    public class FoodItem
    {
        public string itemName { get; private set; }
        public IngredientData ingredientData { get; private set; }
        public Sprite sprite { get; private set; }
        public int value { get; private set; } 
        public int quality { get; private set; }
        public Dictionary<IngredientData, int> ingredientLayerDict
            = new Dictionary<IngredientData, int>();

        public FoodItem(IngredientData ingredientData
            , List<IngredientData> ingredientLayerList)
        {
            this.ingredientData = ingredientData;

            if(ingredientLayerList == null ||
                ingredientLayerList.Count == 0)
            {
                SingleConstruct(ingredientData);
                
            }
            else
            {
                VariantConstruct(ingredientData
                    , ingredientLayerList);
            }
        }

        private void VariantConstruct(IngredientData ingredientData
            , List<IngredientData> ingredientLayerList)
        {
            if (ingredientData.ingredientLayerDict.Count == 0)
            {
                Debug.LogError("Cannot VariantConstruct on empty ingredientLayerList" +
                    "of ingredientData \"" + ingredientData.name + "\"");
                return;
            }

            List<Sprite> toBeLayered = new List<Sprite>();

            foreach(IngredientData ingredient in ingredientLayerList)
            {
                int thisLayer = ingredientData.ingredientLayerDict[ingredient];
                ingredientLayerDict.Add(ingredientData, thisLayer);

                if (ingredient.sprite == null)
                {
                    Debug.LogError("Cannot add sprite to toBeLayered");
                    continue;
                }

                toBeLayered.Add(ingredient.sprite);
            }

            itemName = ingredientData.name;
            sprite = ExtensionMethods.LayerSprite(toBeLayered);

            if(ingredientData.combineMode
                == IngredientData.CombineMode.Additive)
            {
                // Not implemented yet
                value = ingredientData.baseValue;
            }
            if (ingredientData.combineMode
                == IngredientData.CombineMode.BaseOnly)
            {
                value = ingredientData.baseValue;
            }
            quality = 1;
            
        }

        private void SingleConstruct(IngredientData ingredientData)
        {
            itemName = ingredientData.name;
            sprite = ingredientData.sprite;
            value = ingredientData.baseValue;
            quality = 1;
            ingredientLayerDict.Add(ingredientData, 0);
        }
    }
}