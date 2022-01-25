using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Items;

namespace Simmer.Inventory
{
    public class InventoryEventManager : MonoBehaviour
    {
        // First int is new index, ItemBehaviour is item before change
        public UnityEvent<int, ItemBehaviour> OnInventoryChange
            = new UnityEvent<int, ItemBehaviour>();

        public void Construct()
        {

        }
    }
}