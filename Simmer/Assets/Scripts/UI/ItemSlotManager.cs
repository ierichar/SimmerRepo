using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.Inventory
{
    public class ItemSlotManager : MonoBehaviour
    {
        public ItemImageManager inventoryImageManager { get; private set; }
        public ItemCornerTextManager itemCornerTextManager { get; private set; }
        public ItemBackgroundManager itemBackgroundManager { get; private set; }

        public int index { get; private set; }

        public void Construct(int index)
        {
            this.index = index;

            inventoryImageManager = GetComponentInChildren<ItemImageManager>();
            inventoryImageManager.Construct();

            itemBackgroundManager = GetComponentInChildren<ItemBackgroundManager>();
            itemBackgroundManager.Construct();

            itemCornerTextManager = GetComponentInChildren<ItemCornerTextManager>();
            itemCornerTextManager.Construct(index);
        }
    }
}