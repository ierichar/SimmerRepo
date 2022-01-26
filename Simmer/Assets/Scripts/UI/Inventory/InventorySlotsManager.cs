using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;

namespace Simmer.Inventory
{
    public class InventorySlotsManager : MonoBehaviour
    {
        public List<InventorySlotManager> inventorySlotList { get; private set; }
        
        public int maxInventorySize { get; private set; }

        public void Construct(ItemFactory itemFactory
            , InventoryEventManager inventoryEventManager)
        {
            // Will get them in order of Scene Hierarchy from top to bottom
            InventorySlotManager[] inventorySlotManagerArray
                = GetComponentsInChildren<InventorySlotManager>();

            maxInventorySize = inventorySlotManagerArray.Length;

            inventorySlotList = new List<InventorySlotManager>();

            for (int i = 0; i < maxInventorySize; ++i)
            {
                InventorySlotManager thisSlot = inventorySlotManagerArray[i];

                inventorySlotList.Add(thisSlot);
                thisSlot.Construct(inventoryEventManager.OnInventoryChange, itemFactory, i);
            }
        }

        public InventorySlotManager GetInventorySlot(int index)
        {
            return inventorySlotList[index];
        }
    }
}