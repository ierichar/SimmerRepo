using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.FoodData;

namespace Simmer.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        private InventoryManager _inventoryManager;

        [SerializeField] private List<IngredientData> startingIngredients
            = new List<IngredientData>();
        [SerializeField] private int startingQuality;
        [SerializeField] private int maxInventorySize;

        private Dictionary<int, FoodItem> foodItemDictionary
            = new Dictionary<int, FoodItem>();

        private int nextToFillIndex = 0;

        public void Construct(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;

            foreach(IngredientData ingredient in startingIngredients)
            {
                FoodItem newFoodItem = new FoodItem(ingredient, startingQuality);
                AddFoodItem(newFoodItem);
            }
        }

        public void AddFoodItem(FoodItem item)
        {
            nextToFillIndex = GetNextToFillIndex();

            if (nextToFillIndex == -1)
            {
                print("Inventory is full");
            }
            else
            {
                foodItemDictionary.Add(nextToFillIndex, item);

                InventorySlotManager inventorySlot
                    = _inventoryManager.GetInventorySlot(nextToFillIndex);
                inventorySlot.inventoryImageManager
                    .SetSprite(item.ingredientData.sprite);
            } 
        }

        public FoodItem RemoveFoodItem(int index)
        {
            FoodItem removedFoodItem = null;

            if(foodItemDictionary.ContainsKey(index))
            {
                removedFoodItem = foodItemDictionary[index];
                foodItemDictionary.Remove(index);

                InventorySlotManager inventorySlot
                    = _inventoryManager.GetInventorySlot(index);
                inventorySlot.inventoryImageManager
                    .SetSprite(null);
            }

            if(removedFoodItem == null)
            {
                print(this + " Error: RemoveFoodItem index has no FoodItem");
            }
            return removedFoodItem;
        }

        private int GetNextToFillIndex()
        {
            nextToFillIndex = 0;
            for (int i = 0; i < maxInventorySize; ++i)
            {
                if (!foodItemDictionary.ContainsKey(i))
                {
                    return i;
                }
            }
            return -1;
        }


    }
}
