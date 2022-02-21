using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Simmer.FoodData;
using Simmer.UI.RecipeMap;

namespace Simmer.UI.RecipeBook.FoodInfo
{
    public class FoodInfoManager : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private RecipeMapManager _recipeMapManager;

        [SerializeField] private UITextManager _titleText;
        [SerializeField] private UITextManager _infoText;
        [SerializeField] private ImageManager _foodImageManager;
        [SerializeField] private Button _bookmarkButton;
        [SerializeField] private Button _recipeMapButton;
        [SerializeField] private Button _utilityMapButton;

        private IngredientData ingredientData;

        public void Construct()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _recipeMapManager = FindObjectOfType<RecipeMapManager>(true);

            _titleText.Construct();
            _infoText.Construct();
            _foodImageManager.Construct();

            _recipeMapButton.onClick.AddListener(RecipeMapButtonOnClickCallback);
            _utilityMapButton.onClick.AddListener(UtilityMapButtonOnClickCallback);

            _canvasGroup.alpha = 0;
        }

        public void UpdateFoodInfo(IngredientData ingredientData)
        {
            _canvasGroup.alpha = 1;

            this.ingredientData = ingredientData;

            _titleText.SetText(ingredientData.name);
            UpdateInfoText();
            _foodImageManager.SetSprite(ingredientData.sprite);
        }

        private void UpdateInfoText()
        {
            string newInfoText =
                "• Base value: " + ingredientData.baseValue + "\n" +
                "• Utility count: " + ingredientData
                    .applianceRecipeListDict.Count + "\n";

            _infoText.SetText(newInfoText);
        }

        private void RecipeMapButtonOnClickCallback()
        {
            _recipeMapManager.ToggleActive();
            _recipeMapManager.recipeMapEventManager
                .OnShowRecipeMap.Invoke(ingredientData);
        }

        private void UtilityMapButtonOnClickCallback()
        {
            _recipeMapManager.ToggleActive();
            _recipeMapManager.recipeMapEventManager
                .OnShowUtilityMap.Invoke(ingredientData);
        }
    }
}