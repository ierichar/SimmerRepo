using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Simmer.UI;
using Simmer.FoodData;

namespace Simmer.UI.RecipeBook.Catalog
{
    public class CatalogItem : MonoBehaviour
    {
        private RecipeBookEventManager _eventManager;

        private ImageManager _imageManager;
        private UITextManager _textManager;
        private Button _button;

        public IngredientData ingredientData { get; private set; }

        public void Construct(RecipeBookEventManager eventManager
            , IngredientData ingredientData)
        {
            _eventManager = eventManager;

            _imageManager = GetComponentInChildren<ImageManager>();
            _imageManager.Construct();

            _textManager = GetComponentInChildren<UITextManager>();
            _textManager.Construct();

            _button = GetComponent<Button>();

            _button.onClick.AddListener(() => {
                eventManager.OnSelectCatalogueItem
                    .Invoke(this.ingredientData);
            });

            ConstructItem(ingredientData);
        }

        private void ConstructItem(IngredientData ingredientData)
        {
            this.ingredientData = ingredientData;

            _imageManager.SetSprite(ingredientData.sprite);
            _textManager.SetText(ingredientData.name);
        }
    }
}