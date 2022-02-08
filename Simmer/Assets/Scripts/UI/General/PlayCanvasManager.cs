using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Inventory;
using Simmer.Items;
using Simmer.UI.Tooltips;
using Simmer.UI.RecipeMap;

namespace Simmer.UI
{
    public class PlayCanvasManager : MonoBehaviour
    {
        public Canvas playCanvas { get; set; }

        public ItemFactory itemFactory { get; private set; }
        public InventoryUIManager inventoryUIManager { get; private set; }
        public RecipeMapWindow recipeMapWindow { get; private set; }

        public UnityEvent<int> OnSelectItem { get; private set; }

        public void Construct(UnityEvent<int> OnSelectItem)
        {
            this.OnSelectItem = OnSelectItem;
            playCanvas = GetComponent<Canvas>();

            itemFactory = GetComponent<ItemFactory>();
            inventoryUIManager = GetComponentInChildren<InventoryUIManager>(true);
            recipeMapWindow = GetComponentInChildren<RecipeMapWindow>(true);

            itemFactory.Construct(this);
            inventoryUIManager.Construct(itemFactory);
            recipeMapWindow.Construct();


            // TEMP TO TEST ITEMSLOTMANAGER
            PantrySlotGroupManager pantrySlotGroupManager
                = FindObjectOfType<PantrySlotGroupManager>();
            if(pantrySlotGroupManager)
            {
                pantrySlotGroupManager.Construct(itemFactory);
            }

            MixerManager mixerManager = FindObjectOfType<MixerManager>();
            if(mixerManager) mixerManager.Construct();

            PantryUI pantryManager = FindObjectOfType<PantryUI>();
            if(pantryManager) pantryManager.Construct();

            OvenManager ovenManager = FindObjectOfType<OvenManager>();
            if(ovenManager) ovenManager.Construct();

            PlateManager plateManager = FindObjectOfType<PlateManager>();
            if(plateManager) plateManager.Construct();

            
        }
    }
}