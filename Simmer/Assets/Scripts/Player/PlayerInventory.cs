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
        public int selectedItemIndex { get; private set;}
        private Color defaultColor = new Color(204.0f, 204.0f, 204.0f, 255.0f);

        public void Construct(PlayerManager playerManager)
        {
            _inventoryUIManager = playerManager.inventoryUIManager;
            _playerHeldItem = playerManager.playerHeldItem;

            selectedItemIndex = -1;

            foreach (IngredientData ingredient in _startingIngredients)
            {
                FoodItem newFoodItem = new FoodItem(ingredient, _startingQuality);
                AddFoodItem(newFoodItem);
            }

            playerManager.gameEventManager.OnSelectItem
                .AddListener(OnSelectItemCallback);

            playerManager.playerEventManager.OnDropItem
                .AddListener(OnDropItemCallback);

            playerManager.playerEventManager.OnAddRandomItem
                .AddListener(OnAddRandomItemCallback);

            _inventoryUIManager.inventoryEventManager
                .OnInventoryChange.AddListener(OnInventoryChangeCallback);
        }

        private void OnSelectItemCallback(int index)
        {
            ItemSlotManager inventorySlot;
            if (selectedItemIndex >= 0)
            {
                inventorySlot = _inventoryUIManager
                    .inventorySlotsManager.GetInventorySlot(selectedItemIndex);
                inventorySlot.itemBackgroundManager.SetColor(defaultColor);
                _playerHeldItem.SetSprite(null);
            }

            if(selectedItemIndex == index)
            {
                selectedItemIndex = -1;
            }
            else
            {
                selectedItemIndex = index;
                inventorySlot = _inventoryUIManager
                    .inventorySlotsManager.GetInventorySlot(index);
                inventorySlot.itemBackgroundManager.SetColor(Color.yellow);

                UpdateHeldItem();
            }
        }

        private void UpdateHeldItem()
        {
            FoodItem thisFoodItem = GetSelectedItem();
            if (thisFoodItem != null)
            {
                _playerHeldItem.SetSprite(thisFoodItem
                    .ingredientData.sprite);
            }
            else
            {
                _playerHeldItem.SetSprite(null);
            }
        }

        private void OnInventoryChangeCallback(int index, ItemBehaviour itemBehaviour)
        {
            if (itemBehaviour == null)
            {
                _foodItemDictionary.Remove(index);
            }
            else if (_foodItemDictionary.ContainsKey(index))
            {
                //Debug.Log("Trying to change non empty inventory slot, this shouldn't happen");
                // Happens when trying to drag and drop item back in slot it was just in
            }
            else
            {
                _foodItemDictionary.Add(index, itemBehaviour.foodItem);
            }

            UpdateHeldItem();
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

                InventorySlotManager inventorySlot = _inventoryUIManager
                    .inventorySlotsManager.GetInventorySlot(nextToFillIndex);
                inventorySlot.SpawnFoodItem(item);
            } 
        }

        public FoodItem RemoveFoodItem(int index)
        {
            FoodItem removedFoodItem = null;

            if(_foodItemDictionary.ContainsKey(index))
            {
                removedFoodItem = _foodItemDictionary[index];
                _foodItemDictionary.Remove(index);

                ItemSlotManager inventorySlot = _inventoryUIManager
                    .inventorySlotsManager.GetInventorySlot(index);
                inventorySlot.EmptySlot();

                if (selectedItemIndex == index)
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
            for (int i = 0; i < _inventoryUIManager
                .inventorySlotsManager.maxInventorySize; ++i)
            {
                if (!_foodItemDictionary.ContainsKey(i))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool IsFull() 
        {
            if(GetNextToFillIndex() == -1)
            {
                return true;
            }
            return false;
        }
    }
}
