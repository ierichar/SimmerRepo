using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.Inventory
{
    public class InventorySlotManager : MonoBehaviour
    {
        public InventoryImageManager inventoryImageManager { get; private set; }

        public void Construct()
        {
            inventoryImageManager = GetComponentInChildren<InventoryImageManager>();
            inventoryImageManager.Construct();
        }
    }
}