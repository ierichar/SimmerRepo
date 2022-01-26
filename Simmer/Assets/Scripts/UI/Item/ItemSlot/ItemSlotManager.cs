using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Simmer.Items
{
    public class ItemSlotManager : MonoBehaviour, IDropHandler
    {
        public RectTransform rectTransform { get; protected set; }

        public ImageManager itemBackgroundManager { get; protected set; }
        protected ItemFactory _itemFactory;

        public int index { get; protected set; }
        public ItemBehaviour currentItem { get; protected set; }

        private bool _isSelected;

        public virtual void Construct(ItemFactory itemFactory
            , int index)
        {
            rectTransform = GetComponent<RectTransform>();
            this.index = index;

            _itemFactory = itemFactory;

            itemBackgroundManager = GetComponentInChildren<ImageManager>();
            itemBackgroundManager.Construct();
        }

        public ItemBehaviour SpawnFoodItem(FoodItem toSet)
        {
            ItemBehaviour newItem = _itemFactory.ConstructItem(toSet, this);
            SetNewSlot(newItem);
            return newItem;
        }

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                itemBackgroundManager.SetColor(Color.yellow);
            }
            else
            {
                itemBackgroundManager.SetColor(Color.gray);
            }
            _isSelected = selected;
        }

        public virtual void SetItem(ItemBehaviour item)
        {
            currentItem = item;
        }

        public void EmptySlot()
        {
            Destroy(currentItem.gameObject);
            currentItem = null;
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
            if(thisItem.currentSlot == null ) thisItem.currentSlot.EmptySlot();
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