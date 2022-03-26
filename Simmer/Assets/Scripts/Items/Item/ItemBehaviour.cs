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
    /// <summary>
    /// Controls the GameObject behaviour of a draggable item.
    /// Handles all drag events and item visuals.
    /// Constructs an instantiated empty item prefab with necessary
    /// data of type FoodItem and ItemSlotManager.
    /// </summary>    
    public class ItemBehaviour : MonoBehaviour
        , IPointerDownHandler
        , IBeginDragHandler
        , IEndDragHandler
        , IDragHandler
    {
        /// External references given on Construct
        private PlayCanvasManager _playCanvasManager;
        private ItemFactory _itemFactory;
        private TooltipTrigger _tooltipTrigger;
        private Canvas _playCanvas;
        private UnityEvent<int> OnSelectItem;

        /// Internal references retrieved on Construct
        private Canvas _itemCanvas;
        private RectTransform _rectTransform;
        private ImageManager _itemImageManager;
        private CanvasGroup _canvasGroup;

        /// Public variables for internal state
        public ItemSlotManager currentSlot { get; private set; }
        public FoodItem foodItem { get; private set; }
        public UnityEvent<bool> OnChangeSlot = new UnityEvent<bool>();

        /// Private state tracking
        private bool _isChangeSlot;
        private bool _isSelected;
        private Tween activeMoveTween;

        /// <summary>
        /// Constructs an instantiated empty item prefab with necessary
        /// instance data of type FoodItem and ItemSlotManager;
        /// Recieves dependencies from playCanvasManager.
        /// </summary>
        /// <param name="playCanvasManager">
        /// Contains all needed dependencies.
        /// </param>
        /// <param name="foodItem">
        /// FoodItem instance data of this item.
        /// </param>
        /// <param name="startSlot">
        /// ItemSlotManager instance that this this will be contained in.
        /// </param>
        public void Construct(
            PlayCanvasManager playCanvasManager
            , FoodItem foodItem
            , ItemSlotManager startSlot)
        {
            _playCanvasManager = playCanvasManager;
            OnSelectItem = _playCanvasManager.OnSelectItem;
            _playCanvas = _playCanvasManager.playCanvas;
            _itemFactory = playCanvasManager.itemFactory;

            this.foodItem = foodItem;
            this.currentSlot = startSlot;

            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = Vector2.zero;

            _canvasGroup = GetComponent<CanvasGroup>();

            _itemImageManager = GetComponentInChildren<ImageManager>();
            _itemImageManager.Construct();

            _tooltipTrigger = GetComponentInChildren<TooltipTrigger>();
            _tooltipTrigger.Construct(foodItem.itemName, "");

            _itemCanvas = GetComponent<Canvas>();
            _itemCanvas.sortingLayerName = "UI";

            OnChangeSlot.AddListener(OnChangeSlotCallback);

            if (foodItem == null || foodItem.sprite == null)
            {
                //Debug.LogError(this + "Error: Cannot have null foodItem" +
                //    " or foodItem.sprite on Construct");
            }
            else
            {
                _itemImageManager.SetSprite(foodItem.sprite);
            }
            SetCurrentSlot(startSlot);
        }

        /// <summary>
        /// Sets all state variables for this item and the target
        /// item slot. Calls functions to visually move the item
        /// to the slot.
        /// </summary>
        /// <param name="itemSlotManager">
        /// ItemSlotManager to set as currentSlot.
        /// </param>
        public void SetCurrentSlot(ItemSlotManager itemSlotManager)
        {
            currentSlot = itemSlotManager;

            // Parenting the item to the slot makes it so calling
            // TweenToOrigin moves the item to the slot's
            // anchor position on its center
            _rectTransform.SetParent(itemSlotManager.rectTransform);
            TweenToOrigin();

            itemSlotManager.SetItem(this);
        }

        /// <summary>
        /// Primary method for animating items.
        /// Tweens the item to Vector2.zero anchor position.
        /// Gets tween parameters from _itemFactory.
        /// </summary>
        public void TweenToOrigin()
        {
            if (activeMoveTween != null) activeMoveTween.Kill();

            float thisDuration = TweenDuration(Vector2.zero);

            activeMoveTween = _rectTransform.DOAnchorPos
                (Vector2.zero, thisDuration)
                .SetEase(_itemFactory.moveEase)
                .OnComplete(OnResetCallback);
        }

        /// <summary>
        /// Calculates the tween duration based on scaling based on
        /// displacement distance and _itemFactory move variables.
        /// </summary>
        /// <param name="newPosition">
        /// Target end position of tween.
        /// </param>
        private float TweenDuration(Vector2 newPosition)
        {
            Vector2 oldPosition = _rectTransform.anchoredPosition;
            float distance = Vector2.Distance(oldPosition, newPosition);

            float thisDuration = _itemFactory.minMoveDuration;
            if (distance <= _itemFactory.minMoveDistance)
            {
                thisDuration = _itemFactory.minMoveDuration;
            }
            else if (distance >= _itemFactory.maxMoveDistance)
            {
                thisDuration = _itemFactory.maxMoveDuration;
            }
            else
            {
                thisDuration = MathUtil.Rescale(
                    _itemFactory.minMoveDistance
                    , _itemFactory.maxMoveDistance
                    , _itemFactory.minMoveDuration
                    , _itemFactory.maxMoveDuration
                    , distance);
            }

            return thisDuration;
        }

        /// <summary>
        /// Resets _canvasGroup variables to default interactable
        /// </summary>
        private void OnResetCallback()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// Sets _isChangeSlot state flag
        /// </summary>
        private void OnChangeSlotCallback(bool changed)
        {
            _isChangeSlot = changed;
        }

        /// <summary>
        /// blockRaycasts = false to allow drop
        /// event on item slot below to not be blocked
        /// </summary>
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 0.5f;
            _canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// Moves the item position to be on the canvas
        /// scaled position of the player pointer
        /// </summary>
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition
                += eventData.delta / _playCanvas.scaleFactor;
        }

        /// <summary>
        /// Handles end item drag behaviour.
        /// </summary>
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            // If detected item changed slot to an inventory slot, 
            // select the item
            if(_isChangeSlot && currentSlot.GetType()
                == typeof(InventorySlotManager))
            {
                OnSelectItem.Invoke(currentSlot.index);
            }
            // Otherwise move back
            else
            {
                TweenToOrigin();
            }

            // Reset state flag
            _isChangeSlot = false;
        }

        /// <summary>
        /// Handles click item behaviour.
        /// </summary>
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!_isSelected && currentSlot.GetType()
                == typeof(InventorySlotManager))
            {
                OnSelectItem.Invoke(currentSlot.index);
            }
            if (activeMoveTween != null)
            {
                activeMoveTween.Kill();
            }
        }
    }
}