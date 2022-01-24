using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;

namespace Simmer.Inventory
{
    public class InventorySlotsManager : MonoBehaviour
    {
        private List<ItemSlotManager> _inventorySlotManagerList
            = new List<ItemSlotManager>();

        public int maxInventorySize { get; private set; }

        public void Construct(ItemFactory itemFactory)
        {
            // Will get them in order of Scene Hierarchy from top to bottom
            ItemSlotManager[] inventorySlotManagerArray
                = GetComponentsInChildren<ItemSlotManager>();

            maxInventorySize = inventorySlotManagerArray.Length;

            for (int i = 0; i < maxInventorySize; ++i)
            {
                ItemSlotManager thisSlot = inventorySlotManagerArray[i];

                _inventorySlotManagerList.Add(thisSlot);
                thisSlot.Construct(itemFactory, i);
            }
        }

        public ItemSlotManager GetInventorySlot(int index)
        {
            return _inventorySlotManagerList[index];
        }
    }
}