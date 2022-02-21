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
using Simmer.NPC;

public class Shop : MonoBehaviour
{
    public NPC_Data npcData;
    public ShopButton buttonPrefab;

    public PlayerInventory inventory;
    public PlayerCurrency money;

    private Transform buttonContainer;
    private List<ShopButton> allButtons;
    private InteractSlot sellSlot;

    //make all the buttons
    void Start() {
        buttonContainer = GameObject.Find("ShopSlots").GetComponent<Transform>();
        allButtons = new List<ShopButton>();
        sellSlot = FindObjectOfType<InteractSlot>();
        sellSlot.Construct();

        List<IngredientData> selection = npcData.selectRandom(12);

        for(int i = 0; i < 12; i++) {
            ShopButton button = Instantiate(buttonPrefab, buttonContainer);
            button.makeButton(selection[i], GetComponent<Shop>());
            allButtons.Add(button);
        }
    }

    //randomize the selection in the shop
    public void makeNewSelection() {
        List<IngredientData> selection = npcData.selectRandom(allButtons.Count);
        for(int i = 0; i < allButtons.Count; i++) {
            allButtons[i].updateButton(selection[i]);
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

    public void hi() {
        Debug.Log("hi");
    }
}
