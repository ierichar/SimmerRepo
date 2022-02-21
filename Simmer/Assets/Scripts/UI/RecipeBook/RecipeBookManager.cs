using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.UI.RecipeBook.Catalogue;
using Simmer.UI.RecipeBook.FoodInfo;

namespace Simmer.UI.RecipeBook
{
    public class RecipeBookManager : MonoBehaviour
    {
        public RecipeBookEventManager eventManager { get; private set; }

        public CatalogueManager catalogueManager { get; private set; }

        public FoodInfoManager foodInfoManager { get; private set; }


        public void Construct()
        {
            foodInfoManager = FindObjectOfType<FoodInfoManager>(true);
            eventManager = GetComponent<RecipeBookEventManager>();
            catalogueManager = FindObjectOfType<CatalogueManager>(true);

            catalogueManager.Construct(eventManager);
            eventManager.Construct(this);
            foodInfoManager.Construct();
        }
    }
}