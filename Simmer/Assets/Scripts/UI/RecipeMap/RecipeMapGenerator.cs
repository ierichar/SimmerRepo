using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;
using Simmer.Appliance;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapGenerator : MonoBehaviour
    {
        [SerializeField] private IngredientData _currentRootIngredient;

        [SerializeField] private float horizontalSpacing;
        [SerializeField] private float verticalSpacing;

        private RecipeMapManager _recipeMapManager;
        private IngredientNodeFactory _ingredientNodeFactory;

        public void Construct(RecipeMapManager recipeMapManager)
        {
            _recipeMapManager = recipeMapManager;
            _ingredientNodeFactory = recipeMapManager.ingredientNodeFactory;

            RecursiveSpawnTree(_currentRootIngredient, 0);
        }

        private void RecursiveSpawnTree(IngredientData data, int level)
        {
            int edgeCount = data.applianceRecipeDict.Count;

            if(edgeCount == 0)
            {
                return;
            }

            int index = 0;
            foreach (KeyValuePair<ApplianceData, RecipeData> pair
                in data.applianceRecipeDict)
            {
                IngredientData childData = pair.Value.resultIngredient;
                Vector2 childPosition = new Vector2(
                    level * horizontalSpacing
                    , edgeCount * verticalSpacing * index);

                IngredientNode childNode = _ingredientNodeFactory.SpawnIngredientNode
                    (childData, childPosition);

                RecursiveSpawnTree(childData, ++level);

                ++index;
            }
        }
    }
}