using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Simmer.Items;

namespace Simmer.Items
{
    public class ItemSlotManager : MonoBehaviour, IDropHandler
    {
        public RectTransform rectTransform { get; private set; }

        public ItemCornerTextManager itemCornerTextManager { get; private set; }
        public ItemBackgroundManager itemBackgroundManager { get; private set; }
        private ItemFactory _itemFactory;

        public int index { get; private set; }
        public ItemBehaviour currentItem { get; private set; }

        public void Construct(ItemFactory itemFactory, int index)
        {
            rectTransform = GetComponent<RectTransform>();
            this.index = index;

            _itemFactory = itemFactory;

            itemBackgroundManager = GetComponentInChildren<ItemBackgroundManager>();
            itemBackgroundManager.Construct();

            itemCornerTextManager = GetComponentInChildren<ItemCornerTextManager>();
            itemCornerTextManager.Construct(index);
        }

        public void SetFoodItem(FoodItem toSet)
        {
            if (toSet == null)
            {
                Destroy(currentItem.gameObject);
            }
            else
            {
                currentItem = _itemFactory.ConstructItem(toSet, this);
            }
        }

        public void SetCurrentItem(ItemBehaviour itemBehaviour)
        {
            this.currentItem = itemBehaviour;
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
            {
                return;
            }

            ItemBehaviour thisItem = eventData.pointerDrag
                .GetComponent<ItemBehaviour>();


            if (currentItem == null)
            {
                SetNewSlot(thisItem);
            }
            else if (thisItem.currentSlot != null)
            {
                SwapSlots(thisItem);
            }
            else
            {
                thisItem.ResetPositionToCurrentSlot();
            }
        }

        private void SetNewSlot(ItemBehaviour thisItem)
        {
            thisItem.currentSlot.SetCurrentItem(null);
            thisItem.SetCurrentSlot(this);
        }

        private void SwapSlots(ItemBehaviour thisItem)
        {
            currentItem.SetCurrentSlot(thisItem.currentSlot);
            thisItem.SetCurrentSlot(this);
        }
    }
}