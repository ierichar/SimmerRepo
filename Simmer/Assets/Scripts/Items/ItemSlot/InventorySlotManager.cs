using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Simmer.UI;
using Simmer.UI.ImageQueue;

namespace Simmer.Items
{
    public class InventorySlotManager : SpawningSlotManager
    {
        private UnityEvent<int, ItemBehaviour> _OnChangeItem;

        public UITextManager cornerTextManager { get; private set; }
        public QueueTrigger queueTrigger { get; private set; }

        public void Construct(
            UnityEvent<int, ItemBehaviour> OnChangeItem
            , ItemFactory itemFactory
            , int index
            , ImageQueueManager recipeBookQueueManager)
        {
            _OnChangeItem = OnChangeItem;
            base.Construct(itemFactory, index);

            //cornerTextManager = GetComponentInChildren<UITextManager>();
            //cornerTextManager.Construct();
            //cornerTextManager.SetText((index + 1).ToString());

            queueTrigger = GetComponent<QueueTrigger>();
            queueTrigger.Construct(recipeBookQueueManager);
        }

        public override void SetItem(ItemBehaviour item)
        {
            currentItem = item;
            _OnChangeItem.Invoke(index, currentItem);
        }
    }
}