using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Simmer.Items
{
    public class InventorySlotManager : ItemSlotManager
    {
        private UnityEvent<int, ItemBehaviour> _OnChangeItem;

        public ItemCornerTextManager itemCornerTextManager { get; private set; }

        public void Construct(UnityEvent<int, ItemBehaviour> OnChangeItem
            , ItemFactory itemFactory, int index)
        {
            _OnChangeItem = OnChangeItem;
            base.Construct(itemFactory, index);

            itemCornerTextManager = GetComponentInChildren<ItemCornerTextManager>();
            itemCornerTextManager.Construct(index);
        }

        public override void SetItem(ItemBehaviour item)
        {
            currentItem = item;
            _OnChangeItem.Invoke(index, currentItem);

        }
    }
}