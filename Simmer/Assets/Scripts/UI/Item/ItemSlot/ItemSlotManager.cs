using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Simmer.UI;

namespace Simmer.Items
{
    public class ItemSlotManager : MonoBehaviour, IDropHandler
    {
        public RectTransform rectTransform { get; protected set; }

        public ImageManager itemBackgroundManager { get; protected set; }
        protected ItemFactory _itemFactory;

        public int index { get; protected set; }
        public ItemBehaviour currentItem { get; protected set; }

        public virtual void Construct(ItemFactory itemFactory
            , int index)
        {
            rectTransform = GetComponent<RectTransform>();
            this.index = index;

            _itemFactory = itemFactory;

            itemBackgroundManager = GetComponentInChildren<ImageManager>();
            itemBackgroundManager.Construct();
        }

        public void SpawnFoodItem(FoodItem toSet)
        {
            SetNewSlot(_itemFactory.ConstructItem(toSet, this));
        }

        public virtual void SetItem(ItemBehaviour item)
        {
            currentItem = item;
        }

        public void EmptySlot()
        {
            Destroy(currentItem.gameObject);
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
            {
                return;
            }

            ItemBehaviour thisItem = eventData.pointerDrag
                .GetComponent<ItemBehaviour>();
            if (thisItem == null) return;

            if (currentItem == null)
            {
                SetNewSlot(thisItem);
                thisItem.OnChangeSlot.Invoke(true);
            }
            else if (thisItem.currentSlot != null)
            {
                SwapSlots(thisItem);
                thisItem.OnChangeSlot.Invoke(true);
            }
            else
            {
                thisItem.ResetPosition();
            }
        }

        private void SetNewSlot(ItemBehaviour thisItem)
        {
            thisItem.currentSlot.SetItem(null);
            thisItem.SetCurrentSlot(this);
        }

        private void SwapSlots(ItemBehaviour thisItem)
        {
            ItemBehaviour tempItem = currentItem;

            tempItem.currentSlot.SetItem(null);
            thisItem.currentSlot.SetItem(null);

            tempItem.SetCurrentSlot(thisItem.currentSlot);
            thisItem.SetCurrentSlot(this);
        }
    }
}