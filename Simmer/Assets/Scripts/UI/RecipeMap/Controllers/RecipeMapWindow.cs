using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapWindow : MonoBehaviour, IControlUI
    {
        private RecipeMapViewport _recipeMapViewport;

        public void Construct()
        {
            _recipeMapViewport = GetComponentInChildren<RecipeMapViewport>();
            _recipeMapViewport.Construct();
        }

        public void ToggleActive()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}