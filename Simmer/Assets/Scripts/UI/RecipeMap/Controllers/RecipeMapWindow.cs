using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapWindow : MonoBehaviour, IControlUI
    {
        private RecipeMapViewport recipeMapViewport;

        public void Construct()
        {
            recipeMapViewport = GetComponentInChildren<RecipeMapViewport>();
            recipeMapViewport.Construct();
        }

        public void ToggleActive()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}