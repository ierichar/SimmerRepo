using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.UI.Tooltips;
using Simmer.Appliance;

namespace Simmer.UI.RecipeMap
{
    public class EdgeLine : MonoBehaviour
    {
        public ImageManager imageManager { get; private set; }
        private RectTransform _rectTransform;
        private TooltipTrigger _tooltipTrigger;

        public void Construct(Vector2 v1
            , Vector2 v2
            , float verticalSpacing
            , float verticalLineGap
            , ApplianceData applianceData)
        {
            imageManager = GetComponent<ImageManager>();
            imageManager.Construct();

            _tooltipTrigger = GetComponentInChildren<TooltipTrigger>();
            _tooltipTrigger.Construct("Appliance: " + applianceData.name, "");

            _rectTransform = GetComponent<RectTransform>();

            Vector2 dist = v1 - v2;
            Vector2 halfDist = dist / 2;
            
            _rectTransform.rotation
                = Quaternion.Euler(new Vector3(0, 0
                , Mathf.Atan2(dist.x, dist.y) * Mathf.Rad2Deg));

            _rectTransform.anchoredPosition
                = v1
                - halfDist;
                //+ new Vector2(0, verticalSpacing);

            _rectTransform.sizeDelta = new Vector2(
                10, Vector2.Distance(v1, v2));

            //_rectTransform.sizeDelta -= new Vector2(0, verticalLineGap);

            imageManager.SetColor(applianceData.colorCode);
        }
    }
}