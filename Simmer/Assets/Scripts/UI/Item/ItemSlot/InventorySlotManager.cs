using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Simmer.Items
{
    public class InventorySlotManager : ItemSlotManager
    {
        private UnityEvent<InventorySlotManager, ItemBehaviour> _OnInventoryChange;

        public ItemCornerTextManager itemCornerTextManager { get; private set; }

        public void Construct(
            UnityEvent<InventorySlotManager, ItemBehaviour> OnInventoryChange
            , ItemFactory itemFactory, int index)
        {
            _OnInventoryChange = OnInventoryChange;
            base.Construct(itemFactory, index);

            itemCornerTextManager = GetComponentInChildren<ItemCornerTextManager>();
            itemCornerTextManager.Construct(index);
        }

        public override void SetItem(ItemBehaviour item)
        {
            currentItem = item;
            _OnInventoryChange.Invoke(this, currentItem);
        }
    }
}