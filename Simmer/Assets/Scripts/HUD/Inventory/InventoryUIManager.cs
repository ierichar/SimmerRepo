using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.UI;

namespace Simmer.Inventory
{
    public class InventoryUIManager : MonoBehaviour
    {
        public InventorySlotGroupManager inventorySlotsManager { get; private set; }

        public InventoryEventManager inventoryEventManager { get; private set; }

        public void Construct(PlayCanvasManager playCanvasManager)
        {
            inventoryEventManager = GetComponent<InventoryEventManager>();
            inventoryEventManager.Construct();

            inventorySlotsManager = GetComponentInChildren<InventorySlotGroupManager>();
            inventorySlotsManager.Construct(
                playCanvasManager.itemFactory
                , inventoryEventManager
                , playCanvasManager.recipeBookQueueManager);
        }

    }
}