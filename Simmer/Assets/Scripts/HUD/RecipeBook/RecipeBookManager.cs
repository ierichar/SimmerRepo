using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.UI.RecipeBook.Catalog;
using Simmer.UI.RecipeBook.FoodInfo;

namespace Simmer.UI.RecipeBook
{
    public class RecipeBookManager : MonoBehaviour
    {
        public RecipeBookEventManager eventManager { get; private set; }

        public CatalogManager catalogManager { get; private set; }

        public FoodInfoManager foodInfoManager { get; private set; }

        public void Construct()
        {
            foodInfoManager = FindObjectOfType<FoodInfoManager>(true);
            eventManager = GetComponent<RecipeBookEventManager>();
            catalogManager = FindObjectOfType<CatalogManager>(true);

            catalogManager.Construct(eventManager);
            eventManager.Construct(this);
            foodInfoManager.Construct();
        }
    }
}