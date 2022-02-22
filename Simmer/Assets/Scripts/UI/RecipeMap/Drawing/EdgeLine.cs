using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.UI.Tooltips;
using Simmer.Appliance;

namespace Simmer.UI.RecipeMap
{
    public class EdgeLine : MonoBehaviour
    {
        private ImageManager _arrowImageManager;
        private ImageManager _applianceImageManager;

        public ImageManager imageManager { get; private set; }
        private RectTransform _rectTransform;
        private TooltipTrigger _tooltipTrigger;

        public void Construct(Vector2 v1
            , Vector2 v2
            , float verticalSpacing
            , float verticalLineGap
            , ApplianceData applianceData
            , bool showArrow)
        {
            _arrowImageManager = GetComponentsInChildren
                <ImageManager>()[1];
            _arrowImageManager.Construct();

            _applianceImageManager = GetComponentsInChildren
                <ImageManager>()[2];
            _applianceImageManager.Construct();

            imageManager = GetComponent<ImageManager>();
            imageManager.Construct();

            _tooltipTrigger = GetComponentInChildren<TooltipTrigger>();

            _rectTransform = GetComponent<RectTransform>();

            Vector2 dist = new Vector2(v2.x - v1.x, v2.y - v1.y);
            Vector2 halfDist = dist / 2;
            
            _rectTransform.anchoredPosition
                = v1 + halfDist;
            //+ new Vector2(0, verticalSpacing);

            float zRotation = Vector2.SignedAngle(Vector2.up, dist);

            _rectTransform.rotation
                = Quaternion.Euler(new Vector3(0, 0 , zRotation));

            _rectTransform.sizeDelta = new Vector2(
                10, dist.magnitude);

            //_rectTransform.sizeDelta -= new Vector2(0, verticalLineGap);

            _arrowImageManager.SetActive(showArrow);
            _applianceImageManager.SetActive(showArrow);

            if (showArrow)
            {
                _applianceImageManager.SetSprite(applianceData.sprite);
                _applianceImageManager.rectTransform.rotation
                    = Quaternion.Euler(new Vector3(0, 0, 0));

                _arrowImageManager.SetColor(applianceData.colorCode);
            }

            _tooltipTrigger.Construct("Appliance: " + applianceData.name, "");
            imageManager.SetColor(applianceData.colorCode);
        }
    }
}