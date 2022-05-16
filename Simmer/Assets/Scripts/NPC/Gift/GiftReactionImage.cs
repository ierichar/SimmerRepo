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
        private bool locked;

        private Tween _scaleTween;

        public override void Construct()
        {
            base.Construct();
            SetActive(false);
        }

        public IEnumerator ReactionSequence(bool isPositive)
        {
            if(!locked){
                locked = true;
                SetActive(true);
                if (isPositive) SetSprite(_positiveSprite);
                else SetSprite(_negativeSprite);

                image.rectTransform.localScale = Vector2.zero;

                if (_scaleTween != null) _scaleTween.Kill();

                _scaleTween = image.rectTransform.DOScale(1, 0.5f)
                    .SetEase(Ease.OutSine);

                yield return _scaleTween.WaitForCompletion();
                SetActive(false);
                locked = false;
            }
        }
        /// <summary>
        /// ReleaseLock() sets the locked to false
        /// and the active object to false as both of these need to be done
        /// before the coroutine stops. This means you should ONLY call
        /// ReleaseLock() from a class which would prematurly end the coroutine
        /// such as disabling the object
        /// </summary>
        public void ReleaseLock(){
            locked = false;
            SetActive(false);
        }
    }
}