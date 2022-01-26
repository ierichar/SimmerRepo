using Simmer.Inventory;
using Simmer.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Simmer.Items
{
    public class ItemBehaviour : MonoBehaviour
        , IPointerDownHandler
        , IPointerUpHandler
        , IBeginDragHandler
        , IEndDragHandler
        , IDragHandler
    {
        private ItemFactory _itemFactory;
        private PlayCanvasManager _playCanvasManager;
        private UnityEvent<ItemBehaviour> OnSelectItem;
        private Canvas _playCanvas;

        private RectTransform _rectTransform;
        public ItemImageManager itemImageManager { get; private set; }
        public ImageManager borderImageManager { get; private set; }
        private CanvasGroup _canvasGroup;

        public ItemSlotManager currentSlot { get; private set; }
        public FoodItem foodItem { get; private set; }

        public UnityEvent<bool> OnChangeSlot = new UnityEvent<bool>();
        private bool _isChangeSlot;
        private bool _isSelected = false;

        private Tween activeMoveTween;

        public void Construct(ItemFactory itemFactory
            , PlayCanvasManager playCanvasManager
            , FoodItem foodItem
            , ItemSlotManager currentSlot)
        {
            _itemFactory = itemFactory;
            this.foodItem = foodItem;
            this.currentSlot = currentSlot;

            _playCanvasManager = playCanvasManager;
            OnSelectItem = _playCanvasManager.OnSelectItem;
            _playCanvas = _playCanvasManager.playCanvas;

            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = Vector2.zero;

            _canvasGroup = GetComponent<CanvasGroup>();

            borderImageManager = GetComponentInChildren<ImageManager>();
            borderImageManager.Construct();

            itemImageManager = GetComponentInChildren<ItemImageManager>();
            itemImageManager.Construct();

            OnChangeSlot.AddListener(OnChangeSlotCallback);

            if (foodItem == null)
            {
                Debug.LogError(this + "Error: Cannot have null foodItem on Construct");
            }
            else
            {
                itemImageManager.SetSprite(foodItem.ingredientData.sprite);
            }
        }

        public void SetSelected(bool selected)
        {
            if(selected)
            {
                borderImageManager.SetColor(Color.yellow);
            }
            else
            {
                borderImageManager.SetColor(Color.gray);
            }
            _isSelected = selected;
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
            //Vector2 oldPosition = _rectTransform.anchoredPosition;
            //float distance = Vector2.Distance(oldPosition, Vector2.zero);

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

            if(!_isChangeSlot)
            {
                ResetPosition();
            }

            _isChangeSlot = false;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!_isSelected)
            {
                OnSelectItem.Invoke(this);
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

        private void OnChangeSlotCallback(bool changed)
        {
            _isChangeSlot = changed;
        }
    }

}