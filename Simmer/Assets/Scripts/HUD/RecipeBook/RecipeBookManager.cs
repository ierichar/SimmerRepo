using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using Simmer.UI.RecipeBook.Catalog;
using Simmer.UI.RecipeBook.FoodInfo;

namespace Simmer.UI.RecipeBook
{
    public class RecipeBookManager : MonoBehaviour
    {
        protected GameEventManager _gameEventManager;

        protected RectTransform _rectTransform;
        protected Vector3 _offScreenPosition;

        public RecipeBookEventManager eventManager { get; private set; }
        public CatalogManager catalogManager { get; private set; }
        public FoodInfoManager foodInfoManager { get; private set; }

        protected Tween _currentOpenCloseTween;

        public void Construct(GameEventManager gameEventManager)
        {
            _gameEventManager = gameEventManager;

            _rectTransform = GetComponent<RectTransform>();

            _offScreenPosition = new Vector3(0
                , _rectTransform.sizeDelta.y, 0);

            foodInfoManager = FindObjectOfType<FoodInfoManager>(true);
            eventManager = GetComponent<RecipeBookEventManager>();
            catalogManager = FindObjectOfType<CatalogManager>(true);

            catalogManager.Construct(eventManager);
            eventManager.Construct(this);
            foodInfoManager.Construct();
        }

        public void Open()
        {
            _gameEventManager.onInteractUI.Invoke(true);

            _rectTransform.localPosition = _offScreenPosition;
            gameObject.SetActive(true);

            if (_currentOpenCloseTween != null) _currentOpenCloseTween.Kill();
            _currentOpenCloseTween = _rectTransform.DOAnchorPos(
                new Vector3(0, 0, 0), 1.0f)
                .SetEase(Ease.OutSine);
        }

        public void Close()
        {
            if (_currentOpenCloseTween != null) _currentOpenCloseTween.Kill();
            _currentOpenCloseTween = _rectTransform.DOAnchorPos(
                _offScreenPosition, 1.0f)
                .OnComplete(() =>
                {
                    _gameEventManager.onInteractUI.Invoke(false);
                    gameObject.SetActive(false);
                });
        }

        private void OnDestroy()
        {
            if (_currentOpenCloseTween != null) _currentOpenCloseTween.Kill();
        }
    }
}