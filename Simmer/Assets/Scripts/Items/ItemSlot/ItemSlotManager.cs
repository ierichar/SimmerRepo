using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Simmer.UI;
using Simmer.Inventory;

namespace Simmer.Items
{
    public class ItemSlotManager : MonoBehaviour, IDropHandler
    {
        public RectTransform rectTransform { get; protected set; }
        public ImageManager itemBackgroundManager { get; protected set; }

        public int index { get; protected set; }
        public ItemBehaviour currentItem { get; protected set; }
        private LockSprite lockImage;
        // For potential locking of item slots during recipe cooking, action duration time
        // create lockSlot bool
        // initiallize in construct
        // write public function lock(bool lockActive) to change internal bool lockSlot
        // Go to GenericAppliance and call new function when start button is pressed

        public UnityEvent<ItemBehaviour> onItemDrop
            = new UnityEvent<ItemBehaviour>();

        public virtual void Construct(int index)
        {
            rectTransform = GetComponent<RectTransform>();
            this.index = index;
            lockImage = GetComponentInChildren<LockSprite>();

            itemBackgroundManager = GetComponentInChildren<ImageManager>(true);
            itemBackgroundManager.Construct();

            locking(false);
        }

        public virtual void SetItem(ItemBehaviour item)
        {
            currentItem = item;
        }

        public void EmptySlot()
        {
            if(currentItem != null) Destroy(currentItem.gameObject);
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

            onItemDrop.Invoke(thisItem);
        }

        protected void SetNewSlot(ItemBehaviour thisItem)
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

        public void locking(bool setActive){
            lockImage.gameObject.SetActive(setActive);
        }
    }
}