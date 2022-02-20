using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

[CreateAssetMenu(fileName = "New SaveData"
        , menuName = "GlobalData/SaveData")]

public class SaveData : ScriptableObject
{
    public List<IngredientData> startKnownList
        = new List<IngredientData>();

    public List<IngredientData> startInventoryList
        = new List<IngredientData>();
}
