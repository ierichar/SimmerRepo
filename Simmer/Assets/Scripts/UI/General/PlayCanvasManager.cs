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

        public InventoryUIManager inventoryUIManager { get; private set; }
        public ItemFactory itemFactory { get; private set; }

        public UnityEvent<ItemBehaviour> OnSelectItem { get; private set; }

        public void Construct(UnityEvent<ItemBehaviour> OnSelectItem)
        {
            this.OnSelectItem = OnSelectItem;
            playCanvas = GetComponent<Canvas>();

            itemFactory = GetComponent<ItemFactory>();
            itemFactory.Construct(this);

            inventoryUIManager = GetComponentInChildren<InventoryUIManager>();
            inventoryUIManager.Construct(itemFactory);
        }
    }
}