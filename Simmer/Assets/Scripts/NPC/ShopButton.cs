using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simmer.FoodData;

namespace Simmer.UI 
{
    public class ShopButton : MonoBehaviour 
    {
        private Image shopImage;
        private TextMeshProUGUI costText;
        public IngredientData currentIngredient;
        public int cost;
        private Button button;
        public Shop shop;

        //get the right components
        public void makeButton(IngredientData ingredient, Shop s) {
            currentIngredient = ingredient;
            shopImage = gameObject.transform.Find("ItemSprite").GetComponent<Image>();
            costText = gameObject.transform.Find("Cost").GetComponent<TextMeshProUGUI>();
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