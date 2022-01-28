using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Inventory;
using Simmer.Items;

namespace Simmer.UI
{
    public class PlayCanvasManager : MonoBehaviour
    {
        public Canvas playCanvas { get; set; }

        public TooltipBehaviour tooltipBehaviour { get; private set; }
        public ItemFactory itemFactory { get; private set; }
        public InventoryUIManager inventoryUIManager { get; private set; }

        public UnityEvent<int> OnSelectItem { get; private set; }

        public void Construct(UnityEvent<int> OnSelectItem)
        {
            this.OnSelectItem = OnSelectItem;
            playCanvas = GetComponent<Canvas>();

            tooltipBehaviour = GetComponentInChildren<TooltipBehaviour>(true);
            itemFactory = GetComponent<ItemFactory>();
            inventoryUIManager = GetComponentInChildren<InventoryUIManager>(true);

            tooltipBehaviour.Construct();
            itemFactory.Construct(this);
            inventoryUIManager.Construct(itemFactory);
            

            // TEMP TO TEST ITEMSLOTMANAGER
            PantrySlotGroupManager pantrySlotGroupManager
                = FindObjectOfType<PantrySlotGroupManager>();
            if(pantrySlotGroupManager)
            {
                pantrySlotGroupManager.Construct(itemFactory);
            }
        }
    }
}