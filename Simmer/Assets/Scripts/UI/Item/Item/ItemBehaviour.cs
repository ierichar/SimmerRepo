using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

using Simmer.Inventory;
using Simmer.UI;
using Simmer.UI.Tooltips;

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
        private ItemFactory _itemFactory;
        private TooltipTrigger _tooltipTrigger;
        private Canvas _playCanvas;
        private UnityEvent<int> OnSelectItem;

        private Canvas _itemCanvas;

        private RectTransform _rectTransform;
        private ImageManager _itemImageManager;
        private CanvasGroup _canvasGroup;

        public ItemSlotManager currentSlot { get; private set; }
        public FoodItem foodItem { get; private set; }

        public UnityEvent<bool> OnChangeSlot = new UnityEvent<bool>();
        private bool _isChangeSlot;
        private bool _isSelected;

        private Tween activeMoveTween;

        public void Construct(
            PlayCanvasManager playCanvasManager
            , FoodItem foodItem
            , ItemSlotManager currentSlot)
        {
            _playCanvasManager = playCanvasManager;
            _itemFactory = playCanvasManager.itemFactory;

            this.foodItem = foodItem;
            this.currentSlot = currentSlot;

            OnSelectItem = _playCanvasManager.OnSelectItem;
            _playCanvas = _playCanvasManager.playCanvas;

            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = Vector2.zero;

            _canvasGroup = GetComponent<CanvasGroup>();

            _itemImageManager = GetComponentInChildren<ImageManager>();
            _itemImageManager.Construct();

            OnChangeSlot.AddListener(OnChangeSlotCallback);

            _tooltipTrigger = GetComponentInChildren<TooltipTrigger>();
            _tooltipTrigger.Construct(foodItem.ingredientData.name, "");


            _itemCanvas = GetComponent<Canvas>();
            _itemCanvas.sortingLayerName = "UI";

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
            ResetPosition();

            itemSlotManager.SetItem(this);
        }

        public void ResetPosition()
        {
            if (activeMoveTween != null)
            {
                activeMoveTween.Kill();
            }
            Vector2 oldPosition = _rectTransform.anchoredPosition;
            float distance = Vector2.Distance(oldPosition, Vector2.zero);

            //float newValue = MathUtil.Rescale(0, 10000
            //    , _itemFactory.minMoveDistance
            //    , _itemFactory.maxMoveDistance
            //    , distance);

            //print("newValue " + newValue);

            //float thisDuration = 0f;

            activeMoveTween = _rectTransform.DOAnchorPos
                (Vector2.zero, _itemFactory.minMoveDuration)
                .SetEase(_itemFactory.moveEase);
        }

        private void OnChangeSlotCallback(bool changed)
        {
            _isChangeSlot = changed;
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

            if(_isChangeSlot && currentSlot.GetType() == typeof(InventorySlotManager))
            {
                OnSelectItem.Invoke(currentSlot.index);
            }
            else
            {
                ResetPosition();
            }

            _isChangeSlot = false;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!_isSelected && currentSlot.GetType() == typeof(InventorySlotManager))
            {
                OnSelectItem.Invoke(currentSlot.index);
            }
            if (activeMoveTween != null)
            {
                activeMoveTween.Kill();
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            //OnSelectItem.Invoke(currentSlot.index);
        }
    }
}