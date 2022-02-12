using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Simmer.VN
{
    public class VN_ScreenManager : MonoBehaviour
    {
        private VN_Manager manager;

        public RawImage backgroundImage;
        private RectTransform backgroundTransform;
        public Ease backgroundMoveEase;

        public RawImage blackScreen;
        public Ease fadeEase;

        public void Construct(VN_Manager manager)
        {
            this.manager = manager;
            backgroundTransform = backgroundImage.GetComponent<RectTransform>();
        }

        public IEnumerator FadeBlack(float endAlpha, float duration)
        {
            Tween fadeTween = blackScreen.DOFade(endAlpha, duration)
                .SetEase(fadeEase);

            yield return fadeTween.WaitForCompletion();
        }

        public IEnumerator MoveBackground(float newX, float newY, float duration)
        {
            Vector3 newPosition = new Vector3(newX, newY, 0);
            Tween moveTween = backgroundTransform.DOAnchorPos(newPosition, duration)
                .SetEase(backgroundMoveEase);

            yield return moveTween.WaitForCompletion();
        }
    }
}