using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Inventory;
using Simmer.Items;

namespace Simmer.UI
{
    public class PlayCanvasManager : MonoBehaviour
    {
        private Canvas _playCanvas;

        public InventoryUIManager inventoryUIManager { get; private set; }
        public ItemFactory itemFactory { get; private set; }


        public void Construct()
        {
            _playCanvas = GetComponent<Canvas>();

            itemFactory = GetComponent<ItemFactory>();
            itemFactory.Construct(_playCanvas);

            inventoryUIManager = GetComponentInChildren<InventoryUIManager>();
            inventoryUIManager.Construct(itemFactory);
        }
    }
}