using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Simmer.UI.Tooltips
{
    public class TooltipTrigger : MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
    {
        private string _headerText;
        private string _bodyText;
        private RectTransform _rectTransform;

        public void Construct(string bodyText, string headerText)
        {
            _headerText = headerText;
            _bodyText = bodyText;
            _rectTransform = GetComponent<RectTransform>();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            //print(this.name + "OnPointerEnter");
            TooltipBehaviour.instance.Show(_rectTransform,
                _bodyText, _headerText);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            //print(this.name + "OnPointerOnPointerExitEnter");

            TooltipBehaviour.instance.Hide();
        }

        private void OnDisable()
        {
            TooltipBehaviour.instance.Hide();
        }
    }
}