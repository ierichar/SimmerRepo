using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;

namespace Simmer.Inventory
{
    public class InventoryUIManager : MonoBehaviour
    {
        public InventorySlotGroupManager inventorySlotsManager { get; private set; }

        public InventoryEventManager inventoryEventManager { get; private set; }

        public void Construct(ItemFactory itemFactory)
        {
            inventoryEventManager = GetComponent<InventoryEventManager>();
            inventoryEventManager.Construct();

            inventorySlotsManager = GetComponentInChildren<InventorySlotGroupManager>();
            inventorySlotsManager.Construct(itemFactory, inventoryEventManager);
        }

    }
}