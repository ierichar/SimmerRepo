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

        private void VariantConstruct(IngredientData baseIngredient
            , List<IngredientData> thisIngredientList)
        {
            if (baseIngredient.ingredientLayerDict.Count == 0)
            {
                Debug.LogError("Cannot VariantConstruct on empty ingredientLayerList" +
                    "of ingredientData \"" + baseIngredient.name + "\"");
                return;
            }

            if(!thisIngredientList.Contains(baseIngredient))
            {
                thisIngredientList.Add(baseIngredient);
            }

            string combineName = "";

            List<IngredientData.IngredientLayer> layerList
                = new List<IngredientData.IngredientLayer>();

            foreach (IngredientData ingredient in thisIngredientList)
            {                
                int thisLayer = baseIngredient.ingredientLayerDict
                    [ingredient].layerNum;

                ingredientLayerDict.Add(ingredient, thisLayer);

                if (ingredient.sprite == null)
                {
                    Debug.LogError("Cannot add sprite to toBeLayered");
                    continue;
                }

                layerList.Add(baseIngredient.ingredientLayerDict[ingredient]);

                combineName += ingredient.name;
            }

            itemName = combineName;

            List<Sprite> toBeLayered = new List<Sprite>();

            layerList.Sort(delegate(IngredientData.IngredientLayer x,
                IngredientData.IngredientLayer y)
                {
                    if(x.layerNum == y.layerNum)
                    {
                        return 0;
                    }
                    else if(x.layerNum > y.layerNum)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                });

            foreach(var item in layerList)
            {
                if (item.layerSprite == null)
                    toBeLayered.Add(item.ingredientData.sprite);
                else toBeLayered.Add(item.layerSprite);
            }

            sprite = ExtensionMethods.LayerSprite(toBeLayered);

            if(baseIngredient.combineMode
                == IngredientData.CombineMode.Additive)
            {
                // Not implemented yet
                value = baseIngredient.baseValue;
            }
            if (baseIngredient.combineMode
                == IngredientData.CombineMode.BaseOnly)
            {
                value = baseIngredient.baseValue;
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