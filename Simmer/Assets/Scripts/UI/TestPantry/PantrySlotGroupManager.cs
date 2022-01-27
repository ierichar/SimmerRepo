using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.FoodData;

public class PantrySlotGroupManager : MonoBehaviour
{
    [SerializeField] IngredientData ingredient;

    private List<ItemSlotManager> _inventorySlotManagerList
            = new List<ItemSlotManager>();

    public int pantrySize { get; private set; }

    public void Construct(ItemFactory itemFactory)
    {
        // Will get them in order of Scene Hierarchy from top to bottom
        ItemSlotManager[] itemSlotManagerArray
            = GetComponentsInChildren<ItemSlotManager>();

        pantrySize = itemSlotManagerArray.Length;

        for (int i = 0; i < pantrySize; ++i)
        {
            ItemSlotManager thisSlot = itemSlotManagerArray[i];

            _inventorySlotManagerList.Add(thisSlot);
            thisSlot.Construct(itemFactory, i);

            FoodItem newFoodItem = new FoodItem(ingredient);
            thisSlot.SpawnFoodItem(newFoodItem);
        }
    }
}
