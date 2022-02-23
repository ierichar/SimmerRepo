using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Inventory;
using Simmer.FoodData;
using Simmer.Items;
using Simmer.Appliance;
using Simmer.UI;

public static class GlobalPlayerData
{
    public static Dictionary<int, FoodItem>
        foodItemDictionary
    {
        get { return _inventoryItemDictionary; }
        set { foodItemDictionary = _inventoryItemDictionary; }
    }
    private static Dictionary<int, FoodItem> _inventoryItemDictionary
        = new Dictionary<int, FoodItem>();

    public static Dictionary<ApplianceData, List<FoodItem>> AppInvSaveStruct = new Dictionary<ApplianceData, List<FoodItem>>();
    public static List<FoodItem> PantryInventory = new List<FoodItem>();

    public static List<IngredientData> knownIngredientList { get; private set; }

    public static int playerMoney { get; private set; }

    private static bool isConstructed = false;

    public static void Construct(SaveData startingSaveData)
    {
        if (isConstructed) return;

        isConstructed = true;

        knownIngredientList = new List<IngredientData>();

        if (startingSaveData == null)
        {
            Debug.Log("No startingSaveData given");
            return;
        }

        for(int k=0; k<startingSaveData.startingPantry.Count; ++k){
            FoodItem item = new FoodItem(startingSaveData.startingPantry[k], null);
            PantryInventory.Add(item);
        }

        for (int i = 0; i < startingSaveData.startInventoryList.Count; ++i)
        {
            IngredientData ingredient = startingSaveData.startInventoryList[i];
            FoodItem startFoodItem = new FoodItem(ingredient, null);
            _inventoryItemDictionary.Add(i, startFoodItem);

            AddIngredientKnowledge(ingredient);
        }

        foreach(IngredientData ingredient in startingSaveData.startKnownList)
        {
            AddIngredientKnowledge(ingredient);
        }


    }

    public static void SaveInventoryDictionary(
        Dictionary<int, FoodItem> toSave)
    {
        _inventoryItemDictionary = toSave;

        foreach(var pair in _inventoryItemDictionary)
        {
            IngredientData thisIngredient = pair.Value.ingredientData;

            AddIngredientKnowledge(thisIngredient);
        }
    }

    public static bool AddIngredientKnowledge(IngredientData ingredient)
    {
        if (!knownIngredientList.Contains(ingredient))
        {
            knownIngredientList.Add(ingredient);
            return true;
        }

        return false;
    }

    public static void SetMoney(int amount)
    {
        playerMoney = amount;
    }

/*
    public static void SaveApplianceInv(){
        if(_playCanvasManager.GetType() != typeof(KitchenCanvasManager)) return;

        foreach(GenericAppliance appliance in _playCanvasManager.applianceManager){
            AppInvSaveStruct.Add(appliance.GetInventoryItems());
        }
    }
    */
}