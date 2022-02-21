using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;
using Simmer.SceneManagement;

namespace Simmer.UI.RecipeBook.Catalogue
{
    public class CatalogueManager : MonoBehaviour
    {
        private RecipeBookEventManager _eventManager;
        private CatalogueGrid _catalogueGrid;

        [SerializeField] private AllFoodData _allFoodData;

        public void Construct(RecipeBookEventManager eventManager)
        {
            _eventManager = eventManager;

            _catalogueGrid = GetComponentInChildren<CatalogueGrid>(true);
            _catalogueGrid.Construct(eventManager
                , GlobalPlayerData.knownIngredientList);
        }

        private void OnEnable()
        {
            _catalogueGrid.UpdateGrid(GlobalPlayerData.knownIngredientList);
        }
    }
}