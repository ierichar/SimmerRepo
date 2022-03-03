using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using Simmer.FoodData;
using Simmer.UI.Tooltips;

namespace Simmer.UI.ImageQueue
{
    public class QueueItem : MonoBehaviour
    {
        public IngredientData ingredientData { get; private set; }
        public QueueTrigger queueTrigger { get; private set; }
        public ImageQueueManager imageQueueManager { get; private set; }

        private CanvasGroupManager _canvasGroupManager;
        private RectTransform _rectTransform;
        private ImageManager _imageManager;

        private QueuePosition _queuePosition;

        private Tween _currentMoveTween;

        public void Construct(IngredientData ingredientData
            , QueueTrigger queueTrigger
            , ImageQueueManager imageQueueManager)
        {
            this.ingredientData = ingredientData;
            this.queueTrigger = queueTrigger;
            this.imageQueueManager = imageQueueManager;

            _rectTransform = GetComponent<RectTransform>();

            _imageManager = GetComponentInChildren<ImageManager>();
            _imageManager.Construct();
            _imageManager.SetSprite(ingredientData.sprite);

            TooltipTrigger tooltipTrigger = GetComponent<TooltipTrigger>();
            tooltipTrigger.Construct(imageQueueManager
                .itemTooltipHeader + ": " + ingredientData.name, "");

            _canvasGroupManager = GetComponent<CanvasGroupManager>();
            _canvasGroupManager.Construct();

            _rectTransform.SetParent(queueTrigger.rectTransform);
            _rectTransform.anchoredPosition = Vector2.zero;
        }

        public IEnumerator MoveItem(RectTransform newParent)
        {
            _rectTransform.SetParent(newParent);

            if (_currentMoveTween != null) _currentMoveTween.Kill();

            _currentMoveTween = _rectTransform.DOAnchorPos(
                Vector2.zero, imageQueueManager.itemMoveDuration)
                .SetEase(imageQueueManager.itemMoveEase);

            yield return _currentMoveTween.WaitForCompletion();
        }

        public IEnumerator FadeDestroy()
        {
            Tween fadeTween = _canvasGroupManager.Fade
                (0, 1, Ease.InQuad);
            yield return fadeTween.WaitForCompletion();

            Destroy(gameObject);
        }
    }
}