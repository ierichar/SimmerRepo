using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Simmer.FoodData;
using Simmer.SceneManagement;

namespace Simmer.UI.RecipeBook.Catalog
{
    public class CatalogManager : MonoBehaviour
    {
        public RecipeBookEventManager eventManager { get; private set; }

        public CatalogGrid catalogGrid { get; private set; }
        public CatalogFilterManager catalogFilterManager { get; private set; }

        public AllFoodData allFoodData;

        public void Construct(RecipeBookEventManager eventManager)
        {
            this.eventManager = eventManager;

            catalogGrid = GetComponentInChildren<CatalogGrid>(true);
            catalogGrid.Construct(this);

            catalogFilterManager = GetComponent<CatalogFilterManager>();
            catalogFilterManager.Construct(this);
        }

        private void OnEnable()
        {
            catalogGrid.UpdateGrid(GlobalPlayerData.knownIngredientList);
        }
    }
}