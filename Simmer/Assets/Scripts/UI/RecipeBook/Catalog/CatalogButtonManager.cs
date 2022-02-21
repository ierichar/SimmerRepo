using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Simmer.FoodData;

namespace Simmer.UI.RecipeBook.Catalog
{
    public class CatalogButtonManager : MonoBehaviour
    {
        private CatalogGrid _catalogGrid;
        private AllFoodData _allFoodData;

        [SerializeField] private Button _allButton;
        [SerializeField] private Button _rawButton;
        [SerializeField] private Button _finalButton;

        public void Construct(CatalogManager catalogManager)
        {
            _catalogGrid = catalogManager.catalogGrid;
            _allFoodData = catalogManager.allFoodData;

            _allButton.onClick.AddListener(OnClickAllButtonCallback);
            _rawButton.onClick.AddListener(OnClickRawButtonCallback);
            _finalButton.onClick.AddListener(OnClickFinalButtonCallback);
        }

        private void OnClickAllButtonCallback()
        {
            GenericFilterKnownIngredients(
                (IngredientData item) => { return true; });
        }

        private void OnClickRawButtonCallback()
        {
            GenericFilterKnownIngredients( (IngredientData item) =>
            {
                return _allFoodData.rawIngredientList.Contains(item);
            });
        }

        private void OnClickFinalButtonCallback()
        {
            GenericFilterKnownIngredients((IngredientData item) =>
            {
                return _allFoodData.finalIngredientList.Contains(item);
            });
        }

        private List<IngredientData> GenericFilterKnownIngredients
            (Predicate<IngredientData> predicate)
        {
            List<IngredientData> toFilter =
                GlobalPlayerData.knownIngredientList;

            toFilter = toFilter.FindAll(predicate);

            _catalogGrid.UpdateGrid(toFilter);
            return toFilter;
        }
    }
}