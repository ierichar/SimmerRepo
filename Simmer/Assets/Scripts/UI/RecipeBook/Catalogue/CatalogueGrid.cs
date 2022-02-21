using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeBook.Catalogue
{
    public class CatalogueGrid : MonoBehaviour
    {
        private RecipeBookEventManager _eventManager;

        private Transform _gridTransfrom;

        [SerializeField] CatalogueItem catalogueItemPrefab;

        private List<CatalogueItem> _catalogueItemList
            = new List<CatalogueItem>();

        public void Construct(RecipeBookEventManager eventManager
            , List<IngredientData> startIngredientList)
        {
            _eventManager = eventManager;

            _gridTransfrom = GetComponent<Transform>();

            UpdateGrid(startIngredientList);
        }

        public void UpdateGrid(List<IngredientData> ingredientList)
        {
            ClearItems();

            foreach(IngredientData ingredient in ingredientList)
            {
                AddNewItem(ingredient);
            }
        }

        private void AddNewItem(IngredientData ingredientData)
        {
            CatalogueItem newItem = Instantiate(
                catalogueItemPrefab, _gridTransfrom);

            newItem.Construct(_eventManager, ingredientData);

            _catalogueItemList.Add(newItem);
        }

        private void ClearItems()
        {
            if (_catalogueItemList.Count != 0)
            {
                foreach (CatalogueItem item in _catalogueItemList)
                {
                    Destroy(item.gameObject);
                }
                _catalogueItemList.Clear();
            }
        }
    }
}