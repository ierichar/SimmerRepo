using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Simmer.Player;
using Simmer.FoodData;
using Simmer.Items;
using Simmer.Inventory;
using Simmer.UI;

public class Shop : MonoBehaviour
{
    public List<IngredientData> allBasicFood;
    public ShopButton buttonPrefab;

    private Transform buttonContainer;
    private List<ShopButton> allButtons;

    public PlayerInventory inventory;
    public PlayerCurrency money;

    //These are just things to enable and disable
    private GameObject canvas;
    private GameObject text;
    private Image fadeColor;

    //assgin all the variables
    void Awake()
    {
        canvas = GameObject.Find("ShopSlotsCanvas");
        text = GameObject.Find("ShopTitle");
        fadeColor = gameObject.GetComponent<Image>();
        buttonContainer = GameObject.Find("ShopSlotsCanvas").GetComponent<Transform>();
        allButtons = new List<ShopButton>();
    }

    //make all the buttons
    void Start() {
        for(int i = 0; i < 8; i++) {
            ShopButton button = Instantiate(buttonPrefab, buttonContainer);
            button.makeButton(allBasicFood[Random.Range(0, allBasicFood.Count)], GetComponent<Shop>());
            allButtons.Add(button);
        }
        ToggleOff();
    }

    void Update()
    {
        if(Input.GetButtonDown("Shop")){
            if(canvas.activeSelf) {
                ToggleOff();
            } else {
                ToggleOn();
            }
        }
    }

    //toggles the shop
    public void ToggleOn() {
        text.SetActive(true);
        canvas.SetActive(true);
        fadeColor.enabled = true;
        makeNewSelection();
    }

    //toggles the shop off
    public void ToggleOff() {
        text.SetActive(false);
        canvas.SetActive(false);
        fadeColor.enabled = false;
    }

    //randomize the selection in the shop
    private void makeNewSelection() {
        foreach(ShopButton button in allButtons) {
            button.updateButton(allBasicFood[Random.Range(0, allBasicFood.Count)]);
        }
    }

    public void buyItem(IngredientData ingredient, int cost) {
        if(inventory.IsFull()) {
            Debug.Log("Inventory is full");
            return;
        }
        if(money.getAmt() < cost) {
            Debug.Log("Not Enough Money");
            return;
        }
        money.addMoney(-cost);
        inventory.AddFoodItem(new FoodItem(ingredient, null));
    }
}
