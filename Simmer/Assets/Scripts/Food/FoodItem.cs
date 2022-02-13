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
        public Dictionary<IngredientData, int> ingredientCompDict
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

        private void SingleConstruct(IngredientData ingredientData)
        {
            itemName = ingredientData.name;
            sprite = ingredientData.sprite;
            value = ingredientData.baseValue;
            quality = 1;
            ingredientCompDict.Add(ingredientData, 0);
        }

        private void VariantConstruct(IngredientData baseIngredient
            , List<IngredientData> thisIngredientComp)
        {
            if (baseIngredient.ingredientLayerDict.Count == 0)
            {
                Debug.LogError("Cannot VariantConstruct on empty ingredientLayerList" +
                    "of ingredientData \"" + baseIngredient.name + "\"");
                return;
            }

            // Base ingredient is implied in any thisIngredientComp
            // to provide base properties
            if (!thisIngredientComp.Contains(baseIngredient))
            {
                thisIngredientComp.Add(baseIngredient);
            }

            ConstructCombineName(thisIngredientComp);

            ConstructCombineSprite(baseIngredient, thisIngredientComp);

            // Construct ingredientCompDict
            foreach (IngredientData ingredient in thisIngredientComp)
            {
                if(!baseIngredient.ingredientLayerDict.ContainsKey(ingredient))
                {
                    Debug.LogError(this + " Error: Cannot find ingredient \""
                        + ingredient.name + "\" in baseIngredient.ingredientLayerDict \""
                        + baseIngredient.name + "\"");
                    continue;
                }
                int thisLayer = baseIngredient.ingredientLayerDict
                    [ingredient].layerNum;

                ingredientCompDict.Add(ingredient, thisLayer);
            }

            ConstructCombineValue(baseIngredient);

            quality = 1;
        }

        private void ConstructCombineSprite(IngredientData baseIngredient
            , List<IngredientData> thisIngredientComp)
        {
            List<IngredientLayer> layerList
                = new List<IngredientLayer>();
            // Populate layerList
            foreach (IngredientData ingredient in thisIngredientComp)
            {
                if (ingredient.sprite == null)
                {
                    Debug.LogError("Cannot add sprite to toBeLayered");
                    continue;
                }

                layerList.Add(baseIngredient.ingredientLayerDict[ingredient]);
            }

            // Sort layerList by layerNum with lower number first
            layerList.Sort(delegate (IngredientLayer x,
                IngredientLayer y)
            {
                if (x.layerNum == y.layerNum)       return 0;
                else if (x.layerNum > y.layerNum)   return 1;
                else                                return -1;
            });

            // Get sprites from layerList
            List<Sprite> toBeLayered = new List<Sprite>();
            foreach (var item in layerList)
            {
                if (item.layerSprite == null)
                    toBeLayered.Add(item.ingredientData.sprite);
                else toBeLayered.Add(item.layerSprite);
            }

            // Generate and set sprite
            sprite = ExtensionMethods.LayerSprite(toBeLayered);
        }

        private void ConstructCombineName(
            List<IngredientData> thisIngredientList)
        {
            string combineName = "";

            foreach (IngredientData ingredient in thisIngredientList)
            {
                combineName += ingredient.name;
            }

            itemName = combineName;
        }

        private void ConstructCombineValue(IngredientData baseIngredient)
        {
            if (baseIngredient.combineMode
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
        }
    }
}