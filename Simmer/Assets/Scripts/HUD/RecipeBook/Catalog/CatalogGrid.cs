using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeBook.Catalog
{
    public class CatalogGrid : MonoBehaviour
    {
        private RecipeBookEventManager _eventManager;

        private Transform _gridTransfrom;

        [SerializeField] CatalogItem catalogItemPrefab;

        private List<CatalogItem> _catalogItemList
            = new List<CatalogItem>();

        public void Construct(CatalogManager catalogManager)
        {
            _eventManager = catalogManager.eventManager;

            _gridTransfrom = GetComponent<Transform>();

            UpdateGrid(GlobalPlayerData.knownIngredientList);
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
            CatalogItem newItem = Instantiate(
                catalogItemPrefab, _gridTransfrom);

            newItem.Construct(_eventManager, ingredientData);

            _catalogItemList.Add(newItem);
        }

        private void ClearItems()
        {
            if (_catalogItemList.Count != 0)
            {
                foreach (CatalogItem item in _catalogItemList)
                {
                    Destroy(item.gameObject);
                }
                _catalogItemList.Clear();
            }
        }
    }
}