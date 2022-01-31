using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.FoodData;

public class PantrySlotGroupManager : MonoBehaviour
{
    [SerializeField] private List<IngredientData> _startingIngredients
            = new List<IngredientData>();

    private List<ItemSlotManager> _inventorySlotManagerList
            = new List<ItemSlotManager>();

    public int pantrySize { get; private set; }

    public void Construct(ItemFactory itemFactory)
    {
        // Will get them in order of Scene Hierarchy from top to bottom
        ItemSlotManager[] itemSlotManagerArray
            = GetComponentsInChildren<ItemSlotManager>();

        pantrySize = itemSlotManagerArray.Length;

        for (int i = 0; i < 7; ++i)
        {
            ItemSlotManager thisSlot = itemSlotManagerArray[i];

            _inventorySlotManagerList.Add(thisSlot);
            thisSlot.Construct(itemFactory, i);

            FoodItem newFoodItem = new FoodItem(_startingIngredients[i]);
            thisSlot.SpawnFoodItem(newFoodItem);
        }
    }
}
