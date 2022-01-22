using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Player;
using Simmer.Items;
using Simmer.FoodData;

namespace Simmer.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        private InventoryUIManager _inventoryUIManager;
        private PlayerHeldItem _playerHeldItem;

        [SerializeField] private List<IngredientData> _startingIngredients
            = new List<IngredientData>();
        [SerializeField] private int _startingQuality;

        private Dictionary<int, FoodItem> _foodItemDictionary
            = new Dictionary<int, FoodItem>();

        private int nextToFillIndex = 0;
        private int selectedItemIndex = 0;

        public void Construct(PlayerManager playerManager)
        {
            _inventoryUIManager = playerManager.inventoryUIManager;
            _playerHeldItem = playerManager.playerHeldItem;

            foreach (IngredientData ingredient in _startingIngredients)
            {
                FoodItem newFoodItem = new FoodItem(ingredient, _startingQuality);
                AddFoodItem(newFoodItem);
            }

            playerManager.playerEventManager.OnSelectItem
                .AddListener(OnSelectItemCallback);

            playerManager.playerEventManager.OnDropItem
                .AddListener(OnDropItemCallback);

            playerManager.playerEventManager.OnAddRandomItem
                .AddListener(OnAddRandomItemCallback);
        }

        private void OnSelectItemCallback(int index)
        {
            ItemSlotManager inventorySlot;
            if (selectedItemIndex >= 0)
            {
                inventorySlot = _inventoryUIManager.GetInventorySlot(selectedItemIndex);
                inventorySlot.itemBackgroundManager.SetColor(Color.grey);
                _playerHeldItem.SetSprite(null);
            }

            if(selectedItemIndex == index)
            {
                selectedItemIndex = -1;
            }
            else
            {
                selectedItemIndex = index;
                inventorySlot = _inventoryUIManager.GetInventorySlot(index);
                inventorySlot.itemBackgroundManager.SetColor(Color.yellow);

                FoodItem thisFoodItem = GetSelectedItem();
                if (thisFoodItem != null)
                {
                    _playerHeldItem.SetSprite(thisFoodItem
                        .ingredientData.sprite);
                }
            }
        }

        private void OnDropItemCallback()
        {
            RemoveFoodItem(selectedItemIndex);
        }

        private void OnAddRandomItemCallback()
        {
            int randomIndex = Random.Range(0, _startingIngredients.Count);
            FoodItem foodItem = new FoodItem(_startingIngredients[randomIndex]);
            AddFoodItem(foodItem);
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
                _foodItemDictionary.Add(nextToFillIndex, item);

                ItemSlotManager inventorySlot
                    = _inventoryUIManager.GetInventorySlot(nextToFillIndex);
                inventorySlot.inventoryImageManager
                    .SetSprite(item.ingredientData.sprite);
            } 
        }

        public FoodItem RemoveFoodItem(int index)
        {
            FoodItem removedFoodItem = null;

            if(_foodItemDictionary.ContainsKey(index))
            {
                removedFoodItem = _foodItemDictionary[index];
                _foodItemDictionary.Remove(index);

                ItemSlotManager inventorySlot
                    = _inventoryUIManager.GetInventorySlot(index);
                inventorySlot.inventoryImageManager
                    .SetSprite(null);

                if(selectedItemIndex == index)
                {
                    OnSelectItemCallback(index);
                }
            }

            if(removedFoodItem == null)
            {
                print(this + " Error: RemoveFoodItem index has no FoodItem");
            }
            return removedFoodItem;
        }

        public FoodItem GetSelectedItem()
        {
            if (_foodItemDictionary.ContainsKey(selectedItemIndex))
            {
                return _foodItemDictionary[selectedItemIndex];
            }
            return null;
        }

        private int GetNextToFillIndex()
        {
            nextToFillIndex = 0;
            for (int i = 0; i < _inventoryUIManager.maxInventorySize; ++i)
            {
                if (!_foodItemDictionary.ContainsKey(i))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
