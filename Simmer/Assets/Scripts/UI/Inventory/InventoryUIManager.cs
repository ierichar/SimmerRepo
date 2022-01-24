using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;

namespace Simmer.Inventory
{
    public class InventoryUIManager : MonoBehaviour
    {
        public InventorySlotsManager inventorySlotsManager { get; private set; }

        public void Construct(ItemFactory itemFactory)
        {
            inventorySlotsManager = GetComponentInChildren<InventorySlotsManager>();
            inventorySlotsManager.Construct(itemFactory);
        }

    }
}