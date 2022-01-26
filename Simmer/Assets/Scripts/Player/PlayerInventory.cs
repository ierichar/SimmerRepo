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

        private List<ItemBehaviour> _itemBehaviourList = new List<ItemBehaviour>();

        private List<InventorySlotManager> inventorySlotList;

        public ItemBehaviour selectedItemBehaviour { get; private set; }

        public void Construct(PlayerManager playerManager)
        {
            _inventoryUIManager = playerManager.inventoryUIManager;
            _playerHeldItem = playerManager.playerHeldItem;

            inventorySlotList = _inventoryUIManager
                .inventorySlotsManager.inventorySlotList;

            foreach (IngredientData ingredient in _startingIngredients)
            {
                FoodItem newFoodItem = new FoodItem(ingredient, _startingQuality);
                AddFoodItem(newFoodItem);
            }

            playerManager.gameEventManager.OnSelectItem
                .AddListener(OnSelectItemCallback);

            playerManager.playerEventManager.OnSelectInventoryItem
                .AddListener(OnSelectInventoryItemCallback);

            playerManager.playerEventManager.OnDropItem
                .AddListener(OnDropItemCallback);

            playerManager.playerEventManager.OnAddRandomItem
                .AddListener(OnAddRandomItemCallback);

            _inventoryUIManager.inventoryEventManager
                .OnInventoryChange.AddListener(OnInventoryChangeCallback);
        }

        private void OnSelectItemCallback(ItemBehaviour itemBehaviour)
        {
            // Deselect
            if (itemBehaviour == null)
            {
                if (selectedItemBehaviour == null) return;

                selectedItemBehaviour.SetSelected(false);
                selectedItemBehaviour = null;
                return;
            }

            // Deselect old selected
            if (itemBehaviour == selectedItemBehaviour)
            {
                selectedItemBehaviour.SetSelected(false);
                selectedItemBehaviour = null;
            }
            // Change selected
            else
            {
                if (selectedItemBehaviour != null)
                {
                    selectedItemBehaviour.SetSelected(false);
                    selectedItemBehaviour = null;
                }
                // Select new
                itemBehaviour.SetSelected(true);
                selectedItemBehaviour = itemBehaviour;
            }

            UpdateHeldItem();
        }

        private void OnSelectInventoryItemCallback(int index)
        {
            InventorySlotManager trySelect = _inventoryUIManager
                .inventorySlotsManager.GetInventorySlot(index);

            if (trySelect.currentItem == null) return;
            else
            {
                OnSelectItemCallback(trySelect.currentItem);
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

        private void OnInventoryChangeCallback(
            InventorySlotManager inventorySlotManager
            , ItemBehaviour itemBehaviour)
        {
            if (itemBehaviour == null)
            {

            }
            else if (_itemBehaviourList.Contains(itemBehaviour))
            {
                //Debug.Log("Trying to change non empty inventory slot, this shouldn't happen");
                // Happens when trying to drag and drop item back in slot it was just in
            }
            else
            {
                _itemBehaviourList.Add(itemBehaviour);
            }

            UpdateHeldItem();
        }

        private void OnDropItemCallback()
        {
            RemoveSelectedFoodItem();
        }

        private void OnAddRandomItemCallback()
        {
            int randomIndex = Random.Range(0, _startingIngredients.Count);
            FoodItem foodItem = new FoodItem(_startingIngredients[randomIndex]);
            AddFoodItem(foodItem);
        }

        public void AddFoodItem(FoodItem item)
        {
            InventorySlotManager nextSlot = GetNextToFillIndex();

            if (nextSlot == null)
            {
                print("Inventory is full");
            }
            else
            {
                ItemBehaviour newItem = nextSlot.SpawnFoodItem(item);
                _itemBehaviourList.Add(newItem);
            }
        }

        public void RemoveSelectedFoodItem()
        {
            if (selectedItemBehaviour != null)
            {
                _itemBehaviourList.Remove(selectedItemBehaviour);
                if (selectedItemBehaviour.currentSlot != null)
                {
                    selectedItemBehaviour.currentSlot.EmptySlot();
                    
                }
                else
                {
                    Destroy(selectedItemBehaviour.gameObject);
                }
                selectedItemBehaviour = null;
                UpdateHeldItem();
            }
        }

        public FoodItem GetSelectedItem()
        {
            if (selectedItemBehaviour == null) return null;

            return selectedItemBehaviour.foodItem;
        }

        private InventorySlotManager GetNextToFillIndex()
        {
            InventorySlotManager nextSlot = null;
            for (int i = 0; i < _inventoryUIManager
                .inventorySlotsManager.maxInventorySize; ++i)
            {
                if (inventorySlotList[i].currentItem == null)
                {
                    return inventorySlotList[i];
                }
            }
            return nextSlot;
        }
    }
}
