using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

[CreateAssetMenu(fileName = "New SaveData"
        , menuName = "GlobalData/SaveData")]

public class SaveData : ScriptableObject
{
    public int startingMoney;
    public int startingStage;
    public int[] startingTime = new int[3];

    public bool isFirstLoad = true;
    public string characterName;

    public List<IngredientData> startKnownList
        = new List<IngredientData>();

    public List<IngredientData> startInventoryList
        = new List<IngredientData>();

    public List<IngredientData> startingPantry
        = new List<IngredientData>();
}
