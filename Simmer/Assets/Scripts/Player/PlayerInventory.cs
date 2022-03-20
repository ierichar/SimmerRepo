using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Player;
using Simmer.Items;
using Simmer.FoodData;
using Simmer.UI.ImageQueue;

namespace Simmer.Inventory
{
    /// <summary>
    /// Controls and stores the player's current inventory data
    /// while interacting with the InventoryUIManager
    /// and PlayerHeldItem for visuals using UnityEvents
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        private InventoryUIManager _inventoryUIManager;
        private PlayerHeldItem _playerHeldItem;

        [SerializeField] private AllFoodData _allFoodData;

        /// <summary>
        /// Key: Inventory slot index, Value: FoodItem in the slot
        /// </summary>
        private Dictionary<int, FoodItem> _inventoryItemDictionary
            = new Dictionary<int, FoodItem>();

        /// <summary>
        /// Public property version of _inventoryItemDictionary
        /// </summary>
        public Dictionary<int, FoodItem> inventoryItemDictionary
        {
            get { return _inventoryItemDictionary; }
            set { inventoryItemDictionary = _inventoryItemDictionary; }
        }    

        /// <summary>
        /// Index of selected item visually indicated by outline and held item
        /// </summary>
        public int selectedItemIndex { get; private set;}

        public IngredientData variantCake;
        public IngredientData baseCake;
        public IngredientData variantTopping1;
        public IngredientData variantTopping2;

        /// <summary>
        /// Constructs from the PlayerManager, adds starting ingredients
        /// , add listeners for inventory UnityEvents
        /// </summary>
        /// <param name="playerManager">
        /// PlayerManager passes InventoryUIManager, PlayerHeldItem
        /// , and event managers
        /// </param>
        public void Construct(PlayerManager playerManager)
        {
            _inventoryUIManager = playerManager.inventoryUIManager;
            _playerHeldItem = playerManager.playerHeldItem;

            selectedItemIndex = -1;

            // Load from global
            foreach (var pair in GlobalPlayerData.foodItemDictionary)
            {
                AddFoodItem(pair.Value, pair.Key);
            }

            playerManager.gameEventManager.onSelectItem
                .AddListener(OnSelectItemCallback);

            playerManager.playerEventManager.OnDropItem
                .AddListener(OnDropItemCallback);

            playerManager.playerEventManager.OnAddRandomItem
                .AddListener(OnAddRandomItemCallback);

            _inventoryUIManager.inventoryEventManager
                .OnInventoryChange.AddListener(OnInventoryChangeCallback);
        }

        /// <summary>
        /// Updates highlight on selected item and held item sprite
        /// </summary>
        /// <param name="index">
        /// Inventory hotbar index with 0 being the leftmost
        /// </param>
        private void OnSelectItemCallback(int index)
        {
            ItemSlotManager inventorySlot;
            if (selectedItemIndex >= 0)
            {
                inventorySlot = _inventoryUIManager
                    .inventorySlotsManager.GetInventorySlot(selectedItemIndex);

                inventorySlot.itemBackgroundManager.SetColor(Color.clear);

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
                inventorySlot.itemBackgroundManager.SetColor(Color.green);

                UpdateHeldItem();
            }
        }

        private void UpdateHeldItem()
        {
            FoodItem thisFoodItem = GetSelectedItem();
            if (thisFoodItem != null)
            {
                _playerHeldItem.SetSprite(thisFoodItem.sprite);
            }
            else
            {
                _playerHeldItem.SetSprite(null);
            }
        }

        /// <summary>
        /// Updates inventory visuals and data
        /// </summary>
        /// <param name="index">
        /// Inventory index being modified
        /// </param>
        /// param name="itemBehaviour">
        /// The new ItemBehaviour to set at the index.
        /// Given null, removes the item at the index.
        /// </param>
        private void OnInventoryChangeCallback(int index, ItemBehaviour itemBehaviour)
        {
            if (itemBehaviour == null)
            {
                _inventoryItemDictionary.Remove(index);
            }
            else if (_inventoryItemDictionary.ContainsKey(index))
            {
                Debug.Log("Trying to change non empty inventory slot, this shouldn't happen");
                // Happens when trying to drag and drop item back in slot it was just in
            }
            // Inventory slot empty and non-null item being added
            else
            {
                _inventoryItemDictionary.Add(index, itemBehaviour.foodItem);

                // Update ingredient knowledge
                IngredientData thisIngredient =
                    inventoryItemDictionary[index].ingredientData;
                bool isNew = GlobalPlayerData.AddIngredientKnowledge
                           (thisIngredient);
                InventorySlotManager inventorySlot = _inventoryUIManager
                    .inventorySlotsManager.GetInventorySlot(index);

                if (isNew) inventorySlot.queueTrigger
                        .SpawnQueueItem(thisIngredient);
            }

            UpdateHeldItem();
        }

        private void OnDropItemCallback()
        {
            RemoveFoodItem(selectedItemIndex);
        }

        private void OnAddRandomItemCallback()
        {
            // TESTING VARIANT INGREDIENTS

            List<IngredientData> layers = new List<IngredientData>();
            layers.Add(baseCake);
            layers.Add(variantTopping1);
            layers.Add(variantTopping2);

            FoodItem testCake = new FoodItem(variantCake, layers);

            AddFoodItem(testCake);

            //List<IngredientData> ingredients = _allFoodData.allIngredientDataList;
            //int randomIndex = Random.Range(0, ingredients.Count);
            //FoodItem foodItem = new FoodItem(ingredients[randomIndex], null);
            //AddFoodItem(foodItem);
        }

        /// <summary>
        /// Spawns a new ItemBehaviour with FoodItem data at given index
        /// </summary>
        /// <param name="item">
        /// FoodItem data to be spawned
        /// </param>
        /// <param name="index">
        /// Inventory index location to spawn item
        /// </param>
        public void AddFoodItem(FoodItem item, int index)
        {
            _inventoryItemDictionary.Add(index, item);

            InventorySlotManager inventorySlot = _inventoryUIManager
                .inventorySlotsManager.GetInventorySlot(index);
            inventorySlot.SpawnFoodItem(item);
        }

        /// <summary>
        /// Same as AddFoodItem(FoodItem item, int index) except
        /// spawns item in farthest left available spot
        /// </param>
        public void AddFoodItem(FoodItem item)
        {
            int nextToFillIndex = GetNextToFillIndex();

            if (nextToFillIndex == -1)
            {
                print("Inventory is full");
            }
            else
            {
                AddFoodItem(item, GetNextToFillIndex());
            }
        }

        /// <summary>
        /// Deletes the item at the givne index
        /// </summary>
        /// <param name="index">
        /// Inventory index location to remove item
        /// </param>
        public FoodItem RemoveFoodItem(int index)
        {
            FoodItem removedFoodItem = null;

            if(_inventoryItemDictionary.ContainsKey(index))
            {
                removedFoodItem = _inventoryItemDictionary[index];
                _inventoryItemDictionary.Remove(index);

                ItemSlotManager inventorySlot = _inventoryUIManager
                    .inventorySlotsManager.GetInventorySlot(index);
                inventorySlot.EmptySlot();

                // Deselect item if selected and removed
                if (selectedItemIndex == index)
                {
                    OnSelectItemCallback(index);
                }
            }

            if(removedFoodItem == null)
            {
                Debug.LogError(this + " Error: RemoveFoodItem index has no FoodItem");
            }
            return removedFoodItem;
        }

        public FoodItem GetSelectedItem()
        {
            if (_inventoryItemDictionary.ContainsKey(selectedItemIndex))
            {
                return _inventoryItemDictionary[selectedItemIndex];
            }
            return null;
        }

        private int GetNextToFillIndex()
        {
            for (int i = 0; i < _inventoryUIManager
                .inventorySlotsManager.maxInventorySize; ++i)
            {
                if (!_inventoryItemDictionary.ContainsKey(i))
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
