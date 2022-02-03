using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.FoodData;

public class PantrySlotGroupManager : MonoBehaviour
{
    [SerializeField] private List<IngredientData> _startingIngredients
            = new List<IngredientData>();

    private List<SpawningSlotManager> _inventorySlotManagerList
            = new List<SpawningSlotManager>();

    public int pantrySize { get; private set; }

    public void Construct(ItemFactory itemFactory)
    {
        // Will get them in order of Scene Hierarchy from top to bottom
        SpawningSlotManager[] itemSlotManagerArray
            = GetComponentsInChildren<SpawningSlotManager>();

        pantrySize = itemSlotManagerArray.Length;

        for (int i = 0; i < pantrySize; ++i)
        {
            SpawningSlotManager thisSlot = itemSlotManagerArray[i];

            _inventorySlotManagerList.Add(thisSlot);
            thisSlot.Construct(itemFactory, i);

            if(i < _startingIngredients.Count)
            {
                FoodItem newFoodItem = new FoodItem(_startingIngredients[i]);
                if (newFoodItem != null)
                {
                    thisSlot.SpawnFoodItem(newFoodItem);
                }
            }
        }
    }
}
