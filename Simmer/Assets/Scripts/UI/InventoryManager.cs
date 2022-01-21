using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private List<InventorySlotManager> _inventorySlotManagerList
            = new List<InventorySlotManager>();

        public void Construct()
        {
            InventorySlotManager[] inventorySlotManagerArray
                = GetComponentsInChildren<InventorySlotManager>();

            foreach(InventorySlotManager inventorySlot in inventorySlotManagerArray)
            {
                _inventorySlotManagerList.Add(inventorySlot);
                inventorySlot.Construct();
            }
        }

        public InventorySlotManager GetInventorySlot(int index)
        {
            return _inventorySlotManagerList[index];
        }
    }
}