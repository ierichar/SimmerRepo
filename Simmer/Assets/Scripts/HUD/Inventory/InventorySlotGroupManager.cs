using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.UI.ImageQueue;

namespace Simmer.Inventory
{
    public class InventorySlotGroupManager : MonoBehaviour
    {
        private List<InventorySlotManager> _inventorySlotManagerList
            = new List<InventorySlotManager>();

        public int maxInventorySize { get; private set; }

        public void Construct(ItemFactory itemFactory
            , InventoryEventManager inventoryEventManager
            , ImageQueueManager recipeBookQueueManager)
        {
            // Will get them in order of Scene Hierarchy from top to bottom
            InventorySlotManager[] inventorySlotManagerArray
                = GetComponentsInChildren<InventorySlotManager>();

            maxInventorySize = inventorySlotManagerArray.Length;

            for (int i = 0; i < maxInventorySize; ++i)
            {
                InventorySlotManager thisSlot = inventorySlotManagerArray[i];

                _inventorySlotManagerList.Add(thisSlot);
                thisSlot.Construct(
                    inventoryEventManager.OnInventoryChange
                    , itemFactory
                    , i
                    , recipeBookQueueManager);
            }
        }

        public InventorySlotManager GetInventorySlot(int index)
        {
            if(index < _inventorySlotManagerList.Count
                && index >= 0)
            {
                return _inventorySlotManagerList[index];
            }
            else
            {
                Debug.LogError(this + " Error: Cannot GetInventorySlot " +
                    "of index " + index);
                return _inventorySlotManagerList[0];
            }
            
        }
    }
}