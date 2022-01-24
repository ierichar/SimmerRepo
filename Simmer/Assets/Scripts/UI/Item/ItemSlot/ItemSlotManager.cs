using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Simmer.Items
{
    public class ItemSlotManager : MonoBehaviour, IDropHandler
    {
        public RectTransform rectTransform { get; private set; }

        public ItemCornerTextManager itemCornerTextManager { get; private set; }
        public ItemBackgroundManager itemBackgroundManager { get; private set; }
        private ItemFactory _itemFactory;


        private UnityEvent<int, ItemBehaviour> _OnChangeItem;
        public int index { get; private set; }
        public ItemBehaviour currentItem { get; private set; }

        public void Construct(UnityEvent<int, ItemBehaviour> OnChangeItem
            , ItemFactory itemFactory, int index)
        {
            _OnChangeItem = OnChangeItem;
            rectTransform = GetComponent<RectTransform>();
            this.index = index;

            _itemFactory = itemFactory;

            itemBackgroundManager = GetComponentInChildren<ItemBackgroundManager>();
            itemBackgroundManager.Construct();

            itemCornerTextManager = GetComponentInChildren<ItemCornerTextManager>();
            itemCornerTextManager.Construct(index);
        }

        public void SpawnFoodItem(FoodItem toSet)
        {
            SetNewSlot(_itemFactory.ConstructItem(toSet, this));
        }

        public void SetItem(ItemBehaviour item)
        {
            currentItem = item;
            _OnChangeItem.Invoke(index, currentItem);
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
                thisItem.ResetPositionToCurrentSlot();
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