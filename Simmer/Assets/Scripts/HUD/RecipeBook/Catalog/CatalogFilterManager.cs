using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Simmer.FoodData;

namespace Simmer.UI.RecipeBook.Catalog
{
    public class CatalogFilterManager : MonoBehaviour
    {
        private CatalogGrid _catalogGrid;
        private AllFoodData _allFoodData;

        [SerializeField] private Button _allButton;
        [SerializeField] private Button _rawButton;
        [SerializeField] private Button _finalButton;
        [SerializeField] private TMP_InputField _searchBar;

        char[] _searchSeparators = new char[] { ' ', ',', '\t' };

        private string _currentSearch;

        public void Construct(CatalogManager catalogManager)
        {
            _catalogGrid = catalogManager.catalogGrid;
            _allFoodData = catalogManager.allFoodData;

            _allButton.onClick.AddListener(OnClickAllButtonCallback);
            _rawButton.onClick.AddListener(OnClickRawButtonCallback);
            _finalButton.onClick.AddListener(OnClickFinalButtonCallback);

            _searchBar.onValueChanged.AddListener(OnSearchCallback);
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

        private void OnSearchCallback(string newSearch)
        {
            print("OnSearchCallback");

            List<IngredientData> possibleIngredientList
                 = new List<IngredientData>();

            newSearch = newSearch.ToLower();

            string[] keywordArray = newSearch.Split(_searchSeparators,
                StringSplitOptions.RemoveEmptyEntries);

            if (keywordArray.Length == 0)
            {
                keywordArray = new string[] { newSearch };
            }

            foreach (string searchKeyword in keywordArray)
            {
                _currentSearch = searchKeyword;

                List<IngredientData> possibleSubset =
                    GlobalPlayerData.knownIngredientList
                    .FindAll(SearchIngredientPredicate);

                possibleIngredientList.AddRange(possibleSubset);
            }

            GenericFilterKnownIngredients((IngredientData item) =>
            {
                return possibleIngredientList.Contains(item);
            });
        }

        private bool SearchIngredientPredicate(IngredientData item)
        {
            string formatedString = item.name.ToLower();
            return formatedString.Contains(_currentSearch);
        }

    }
}