using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class IngredientNode : MonoBehaviour
    {
        private ImageManager _borderImageManager;
        private ImageManager _ingredientImageManager;
        private RectTransform _rectTransform;

        public IngredientData ingredientData { get; private set; }

        public void Construct(IngredientData ingredient, Vector2 position)
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

            _ingredientImageManager.SetSprite(this.ingredientData.sprite);
        }
    }
}