using Simmer.Inventory;
using Simmer.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Simmer.Items
{
    public class ItemBehaviour : MonoBehaviour
        , IPointerDownHandler
        , IPointerUpHandler
        , IBeginDragHandler
        , IEndDragHandler
        , IDragHandler
    {
        private PlayCanvasManager _playCanvasManager;
        private UnityEvent<int> OnSelectItem;
        private Canvas _playCanvas;

        private RectTransform _rectTransform;
        private ItemImageManager _itemImageManager;
        private CanvasGroup _canvasGroup;

        public ItemSlotManager currentSlot { get; private set; }
        public FoodItem foodItem { get; private set; }

        public UnityEvent<bool> OnChangeSlot = new UnityEvent<bool>();
        private bool _isChangeSlot;
        private bool _isSelected;

        public void Construct(PlayCanvasManager playCanvasManager
            , FoodItem foodItem
            , ItemSlotManager currentSlot)
        {
            this.foodItem = foodItem;
            this.currentSlot = currentSlot;

            _playCanvasManager = playCanvasManager;
            OnSelectItem = _playCanvasManager.OnSelectItem;
            _playCanvas = _playCanvasManager.playCanvas;

            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = Vector2.zero;

            _canvasGroup = GetComponent<CanvasGroup>();

            _itemImageManager = GetComponentInChildren<ItemImageManager>();
            _itemImageManager.Construct();

            OnChangeSlot.AddListener(OnChangeSlotCallback);

            if (foodItem == null)
            {
                Debug.LogError(this + "Error: Cannot have null foodItem on Construct");
            }
            else
            {
                _itemImageManager.SetSprite(foodItem.ingredientData.sprite);
            }
        }

        public void SetCurrentSlot(ItemSlotManager itemSlotManager)
        {
            currentSlot = itemSlotManager;

            _rectTransform.SetParent(itemSlotManager.rectTransform);
            _rectTransform.anchoredPosition = Vector2.zero;

            itemSlotManager.SetItem(this);
        }

        public void ResetPositionToCurrentSlot()
        {
            _rectTransform.anchoredPosition = Vector2.zero;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 0.5f;
            _canvasGroup.blocksRaycasts = false;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition
                += eventData.delta / _playCanvas.scaleFactor;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;

            if(_isChangeSlot)
            {
                OnSelectItem.Invoke(currentSlot.index);
            }
            else
            {
                ResetPositionToCurrentSlot();
            }

            _isChangeSlot = false;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!_isSelected)
            {
                OnSelectItem.Invoke(currentSlot.index);
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            //OnSelectItem.Invoke(currentSlot.index);
        }

        private void OnChangeSlotCallback(bool changed)
        {
            _isChangeSlot = changed;
        }
    }

}