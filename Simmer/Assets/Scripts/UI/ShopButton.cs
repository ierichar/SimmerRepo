using UnityEngine;
using Simmer.Inventory;
using Simmer.Player;
using Simmer.Items;
using Simmer.FoodData;

class ShopButton : MonoBehaviour 
{
    public IngredientData food;
    public PlayerInventory inventory;
    public PlayerCurrency money;

    public void buyItem() 
    {
        if(inventory.IsFull())
        {
            Debug.Log("Inventory is full");
            return;
        }
        if((money.getAmt() < 20)) 
        {
            Debug.Log("Not Enough Money");
            return;
        }
        money.addMoney(-20);
        inventory.AddFoodItem(new FoodItem(food));
    }
}