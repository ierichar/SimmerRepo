using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simmer.FoodData;
using Simmer.UI.Tooltips;

namespace Simmer.UI 
{
    public class ShopButton : MonoBehaviour 
    {
        public Shop shop;
        public IngredientData currentIngredient;
        public int cost;

        private Image shopImage;
        private TextMeshProUGUI costText;
        private Button button;
        private TooltipTrigger _tooltipTrigger;

        //get the right components
        public void makeButton(IngredientData ingredient, Shop s) {
            currentIngredient = ingredient;
            shopImage = gameObject.transform.Find("ItemSprite").GetComponent<Image>();
            costText = gameObject.transform.Find("Cost").GetComponent<TextMeshProUGUI>();
            _tooltipTrigger = GetComponent<TooltipTrigger>();
            _tooltipTrigger.Construct(currentIngredient.name, "");
            shop = s;

            button = GetComponent<Button>();
            button.onClick.AddListener(() => {
                clicked();
            });
            updateButton(ingredient);
        }

        //Update the image and text based on what ingredient was used as parameter
        public void updateButton(IngredientData ingredient) {
            currentIngredient = ingredient;
            shopImage.sprite = currentIngredient.sprite;
            cost = currentIngredient.baseValue;
            costText.text = "cost: " + cost;
        }

        private void clicked() {
            shop.buyItem(currentIngredient, cost);
        }
    }
}