using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Simmer.Items
{
    public class InventorySlotManager : ItemSlotManager
    {
        public ItemCornerTextManager itemCornerTextManager { get; private set; }

        public override void Construct(UnityEvent<int, ItemBehaviour> OnChangeItem
            , ItemFactory itemFactory, int index)
        {
            base.Construct(OnChangeItem, itemFactory, index);

            itemCornerTextManager = GetComponentInChildren<ItemCornerTextManager>();
            itemCornerTextManager.Construct(index);
        }
    }
}