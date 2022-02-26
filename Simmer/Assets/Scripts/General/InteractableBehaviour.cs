using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace  Simmer.Interactable
{
    public class InteractableBehaviour : MonoBehaviour
    {
        public UnityEvent OnInteract = new UnityEvent();

        private SpriteRendererManager _highlightTarget;

        public void Construct(UnityAction OnInteractCallback
            , SpriteRendererManager highlightTarget)
        {
            OnInteract.AddListener(OnInteractCallback);

            _highlightTarget = highlightTarget;
            StopHighlight();
        }
        //public void Construct(UnityAction OnInteractCallback){
        //    OnInteract.AddListener(OnInteractCallback);
        //    _highlightTarget = null;
        //}

        public void Interact()
        {
            OnInteract.Invoke();
        }

        public void StartHighlight()
        {
            _highlightTarget.SetColor(Color.yellow);
        }

        public void StopHighlight()
        {
            _highlightTarget.SetColor(Color.clear);
        }
    }
}
