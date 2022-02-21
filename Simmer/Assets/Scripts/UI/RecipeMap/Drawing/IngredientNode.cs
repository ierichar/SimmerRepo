using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.UI.Tooltips;
using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class IngredientNode : MonoBehaviour
    {
        private ImageManager _borderImageManager;
        private ImageManager _ingredientImageManager;
        private RectTransform _rectTransform;
        private TooltipTrigger _tooltipTrigger;

        public IngredientData ingredientData { get; private set; }

        public void Construct(
            IngredientData ingredient
            , Vector2 position)
        {
            this.ingredientData = ingredient;

            ImageManager[] ImageManagerArray
                = GetComponentsInChildren<ImageManager>();
            _borderImageManager = ImageManagerArray[0];
            _borderImageManager.Construct();
            _ingredientImageManager = ImageManagerArray[1];
            _ingredientImageManager.Construct();

            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = position;

            _tooltipTrigger = GetComponentInChildren<TooltipTrigger>();

            if (ingredient == null)
            {
                _tooltipTrigger.Construct("Unknown Item", "");
            }
            else
            {
                _ingredientImageManager.SetSprite(this.ingredientData.sprite);
                _tooltipTrigger.Construct("Ingredient: " + ingredientData.name, "");
            }
        }
    }
}