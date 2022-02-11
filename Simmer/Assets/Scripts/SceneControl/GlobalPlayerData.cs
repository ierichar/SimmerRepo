using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Inventory;
using Simmer.FoodData;
using Simmer.Items;

public static class GlobalPlayerData
{
    public static Dictionary<int, FoodItem>
        foodItemDictionary
    {
        get { return _foodItemDictionary; }
        set { foodItemDictionary = _foodItemDictionary; }
    }

    private static Dictionary<int, FoodItem> _foodItemDictionary
        = new Dictionary<int, FoodItem>();


    public static void SaveInventoryDictionary(
        Dictionary<int, FoodItem> toSave)
    {
        _foodItemDictionary = toSave;
    }
}