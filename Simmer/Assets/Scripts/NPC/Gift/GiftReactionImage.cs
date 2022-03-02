using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using Simmer.UI;

namespace Simmer.NPC
{
    public class GiftReactionImage : ImageManager
    {
        [SerializeField] private Sprite _positiveSprite;
        [SerializeField] private Sprite _negativeSprite;

        private Tween _scaleTween;

        public override void Construct()
        {
            base.Construct();
            SetActive(false);
        }

        public IEnumerator ReactionSequence(bool isPositive)
        {
            SetActive(true);
            if (isPositive) SetSprite(_positiveSprite);
            else SetSprite(_negativeSprite);

            image.rectTransform.localScale = Vector2.zero;

            if (_scaleTween != null) _scaleTween.Kill();

            _scaleTween = image.rectTransform.DOScale(1, 0.5f)
                .SetEase(Ease.OutSine);

            yield return _scaleTween.WaitForCompletion();
        }
    }
}