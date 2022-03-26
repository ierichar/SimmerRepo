using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Simmer.UI;
using Simmer.Inventory;

namespace Simmer.Items
{
    /// <summary>
    /// Base class & simplest form of an item slot. Stores the data and
    /// visuals for an ItemBehaviour and handles drop interaction
    /// </summary>
    public class ItemSlotManager : MonoBehaviour, IDropHandler
    {
        public RectTransform rectTransform { get; protected set; }
        public ImageManager itemBackgroundManager { get; protected set; }

        /// <summary>
        /// Int identifier for item slots. Able to get item slots
        /// in a collection by index if given collection unique indexes.
        /// </summary>
        public int index { get; protected set; }
        public ItemBehaviour currentItem { get; protected set; }
        private LockSprite lockImage;
        // For potential locking of item slots during recipe cooking, action duration time
        // create lockSlot bool
        // initiallize in construct
        // write public function lock(bool lockActive) to change internal bool lockSlot
        // Go to GenericAppliance and call new function when start button is pressed

        /// <summary>
        /// Invoked when an item is dragged onto this. Listeners update
        /// visual and data storage on affected item slot(s).
        /// </summary>
        public UnityEvent<ItemBehaviour> onItemDrop
            = new UnityEvent<ItemBehaviour>();

        /// <summary>
        /// At base, needs an index to force need for identification
        /// </summary>
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

        /// <summary>
        /// Gets dragged item and decides to set it's slot to this one,
        /// swap items' slots, or return the item
        /// </summary>
        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
            {
                return;
            }

            // Only continue if dragging an ItemBehaviour
            ItemBehaviour thisItem = eventData.pointerDrag
                .GetComponent<ItemBehaviour>();
            if (thisItem == null) return;

            // Empty slot: Set item to this slot
            if (currentItem == null)
            {
                SetNewSlot(thisItem);
                thisItem.OnChangeSlot.Invoke(true);
            }
            // Non-empty slot: Swap items
            else if (thisItem.currentSlot != null)
            {
                SwapSlotItems(thisItem);
                thisItem.OnChangeSlot.Invoke(true);
            }
            // Other cases: Return item
            else
            {
                thisItem.TweenToOrigin();
            }

            onItemDrop.Invoke(thisItem);
        }

        protected void SetNewSlot(ItemBehaviour thisItem)
        {
            thisItem.currentSlot.SetItem(null);
            thisItem.SetCurrentSlot(this);
        }

        /// <summary>
        /// Sets this slot's currentItem to a given incomingItem slot
        /// and sets the incomingItem to this slot
        /// </summary>
        /// <param name="incomingItem">
        /// The incoming item to be put on this item slot
        /// </param>
        private void SwapSlotItems(ItemBehaviour incomingItem)
        {
            ItemBehaviour oldItem = currentItem;

            oldItem.currentSlot.SetItem(null);
            incomingItem.currentSlot.SetItem(null);

            // thisItem.currentSlot is thisItem's old slot since
            // this line of code is before thisItem.SetCurrentSlot(this);
            oldItem.SetCurrentSlot(incomingItem.currentSlot);
            incomingItem.SetCurrentSlot(this);
        }

        public void locking(bool setActive){
            lockImage.gameObject.SetActive(setActive);
        }
    }
}