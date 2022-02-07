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

        public void Construct(string bodyText, string headerText)
        {
            _headerText = headerText;
            _bodyText = bodyText;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            TooltipBehaviour.instance.Show(_bodyText, _headerText);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            TooltipBehaviour.instance.Hide();
        }
    }
}