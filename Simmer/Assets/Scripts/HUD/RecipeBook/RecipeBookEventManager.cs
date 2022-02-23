using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.FoodData;

namespace Simmer.UI.RecipeBook
{
    public class RecipeBookEventManager : MonoBehaviour
    {
        private RecipeBookManager _recipeBookManager;

        public UnityEvent<IngredientData> OnSelectCatalogueItem
            = new UnityEvent<IngredientData>();

        public void Construct(RecipeBookManager recipeBookManager)
        {
            _recipeBookManager = recipeBookManager;

            OnSelectCatalogueItem.AddListener(OnSelectCatalogueItemCallback);
        }

        private void OnSelectCatalogueItemCallback(IngredientData ingredient)
        {
            _recipeBookManager.foodInfoManager.UpdateFoodInfo(ingredient);
        }
    }
}