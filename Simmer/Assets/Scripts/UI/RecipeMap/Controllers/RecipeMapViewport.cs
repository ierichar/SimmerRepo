using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapViewport : MonoBehaviour
    {
        private RecipeMapManager recipeMapManager;

        public void Construct()
        {
            recipeMapManager = GetComponentInChildren<RecipeMapManager>();
            recipeMapManager.Construct();
        }
    }
}