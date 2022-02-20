using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
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
    public GameObject shopSlots;

    private InteractSlot sellSlot;

    //These are just things to enable and disable
    private GameObject canvas;
    //private GameObject text;
    private Image fadeColor;

    //assgin all the variables
    public void Construct()
    {
        //canvas = GameObject.Find("ShopSlots");
        canvas = shopSlots;
        //text = GameObject.Find("ShopTitle");
        fadeColor = gameObject.GetComponent<Image>();
        //buttonContainer = GameObject.Find("ShopSlots").GetComponent<Transform>();
        buttonContainer = shopSlots.GetComponent<Transform>();
        allButtons = new List<ShopButton>();
        sellSlot = FindObjectOfType<InteractSlot>(true);
        sellSlot.Construct();
        // sellSlot.itemSlot.onItemDrop.AddListener(sellItemWrapper);

        for (int i = 0; i < 12; i++)
        {
            ShopButton button = Instantiate(buttonPrefab, buttonContainer);
            button.makeButton(allBasicFood[Random.Range(0, allBasicFood.Count)], GetComponent<Shop>());
            allButtons.Add(button);
        }
    }

    //make all the buttons
    void Start() {
        
        // ToggleOff();
    }

    // void Update()
    // {
    //     if(Input.GetButtonDown("Shop")){
    //         if(canvas.activeSelf) {
    //             ToggleOff();
    //         } else {
    //             ToggleOn();
    //         }
    //     }
    // }

    //toggles the shop
    public void ToggleOn() {
        makeNewSelection();
    }

    //toggles the shop off
    public void ToggleOff() {
        //text.SetActive(false);
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

    public void sellItemWrapper() {
        if(sellSlot.itemSlot.currentItem == null) return;
        sellItem(sellSlot.itemSlot.currentItem);
    }

    public void sellItem(ItemBehaviour item) {
        Debug.Log("sold item: " + item.foodItem.ingredientData.name);
        money.addMoney(item.foodItem.ingredientData.baseValue);
        sellSlot.itemSlot.EmptySlot();
    }
}
