using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Player;
using Simmer.Items;
using Simmer.FoodData;

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

        //[Tooltip("Player will have these ingredients in their hotbar on start")]
        //[SerializeField] private List<IngredientData> _startingIngredients
        //    = new List<IngredientData>();
        //[Tooltip("_startingIngredients will have their quality set to this")]
        //[SerializeField] private int _startingQuality;

        /// <summary>
        /// Key: Inventory slot index, Value: FoodItem in the slot
        /// </summary>
        private Dictionary<int, FoodItem> _foodItemDictionary
            = new Dictionary<int, FoodItem>();

        /// <summary>
        /// Public property version of _foodItemDictionary
        /// </summary>
        public Dictionary<int, FoodItem> foodItemDictionary
        {
            get { return _foodItemDictionary; }
            set { foodItemDictionary = _foodItemDictionary; }
        }    

        /// <summary>
        /// Index of selected item visually indicated by outline and held item
        /// </summary>
        public int selectedItemIndex { get; private set;}
        private Color defaultColor = new Color(204.0f, 204.0f, 204.0f, 255.0f);

        public IngredientData variantCake;

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

            // Starting items old script
            //foreach (IngredientData ingredient in _startingIngredients)
            //{
            //    FoodItem newFoodItem = new FoodItem(ingredient, _startingQuality);
            //    AddFoodItem(newFoodItem);
            //}

            playerManager.gameEventManager.OnSelectItem
                .AddListener(OnSelectItemCallback);

            playerManager.playerEventManager.OnDropItem
                .AddListener(OnDropItemCallback);

            playerManager.playerEventManager.OnAddRandomItem
                .AddListener(OnAddRandomItemCallback);

            _inventoryUIManager.inventoryEventManager
                .OnInventoryChange.AddListener(OnInventoryChangeCallback);

            // TESTING VARIANT INGREDIENTS

            List<IngredientData> layers = new List<IngredientData>();
            layers.Add(variantCake);
            layers.Add(variantCake.ingredientLayerList[0].ingredientData);
            layers.Add(variantCake.ingredientLayerList[1].ingredientData);

            FoodItem testCake = new FoodItem(variantCake, layers);

            AddFoodItem(testCake);
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
            List<IngredientData> ingredients = _allFoodData.allIngredientDataList;
            int randomIndex = Random.Range(0, ingredients.Count);
            FoodItem foodItem = new FoodItem(ingredients[randomIndex], null);
            AddFoodItem(foodItem);
        }

        public void AddFoodItem(FoodItem item, int index)
        {
            _foodItemDictionary.Add(index, item);

            InventorySlotManager inventorySlot = _inventoryUIManager
                .inventorySlotsManager.GetInventorySlot(index);
            inventorySlot.SpawnFoodItem(item);
        }

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
