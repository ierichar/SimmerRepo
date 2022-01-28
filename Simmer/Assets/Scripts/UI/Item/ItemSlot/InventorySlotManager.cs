using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Simmer.UI;

namespace Simmer.Items
{
    public class InventorySlotManager : ItemSlotManager
    {
        private UnityEvent<int, ItemBehaviour> _OnChangeItem;

        public UITextManager cornerTextManager { get; private set; }

        public void Construct(UnityEvent<int, ItemBehaviour> OnChangeItem
            , ItemFactory itemFactory, int index)
        {
            _OnChangeItem = OnChangeItem;
            base.Construct(itemFactory, index);

            cornerTextManager = GetComponentInChildren<UITextManager>();
            cornerTextManager.Construct();
            cornerTextManager.SetText((index + 1).ToString());
        }

        public override void SetItem(ItemBehaviour item)
        {
            currentItem = item;
            _OnChangeItem.Invoke(index, currentItem);

        }
    }
}