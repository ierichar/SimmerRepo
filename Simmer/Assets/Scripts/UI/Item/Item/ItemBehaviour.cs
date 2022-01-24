using Simmer.Inventory;
using Simmer.UI;
using UnityEngine;

namespace Simmer.Items
{
    public class ItemBehaviour : MonoBehaviour
    {
        public RectTransform rectTransform { get; private set; }
        private ItemImageManager _itemImageManager;
        private Draggable _draggable;

        public ItemSlotManager currentSlot { get; private set; }

        public FoodItem foodItem { get; private set; }

        public void Construct(Canvas playCanvas
            , FoodItem foodItem
            , ItemSlotManager currentSlot)
        {
            this.foodItem = foodItem;
            this.currentSlot = currentSlot;

            rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;

            _draggable = GetComponent<Draggable>();
            _draggable.Construct(playCanvas);

            _itemImageManager = GetComponentInChildren<ItemImageManager>();
            _itemImageManager.Construct();

            if (foodItem == null)
            {
                _itemImageManager.SetSprite(null);
            }
            else
            {
                _itemImageManager.SetSprite(foodItem.ingredientData.sprite);
            }
        }

        public void SetCurrentSlot(ItemSlotManager itemSlotManager)
        {
            currentSlot = itemSlotManager;

            rectTransform.SetParent(itemSlotManager.rectTransform);
            rectTransform.anchoredPosition = Vector2.zero;

            itemSlotManager.SetCurrentItem(this);
        }

        public void ResetPositionToCurrentSlot()
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

}