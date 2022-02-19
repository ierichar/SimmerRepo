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

        private SpriteRenderer _highlightTarget;

        public void Construct(UnityAction OnInteractCallback
            , SpriteRenderer highlightTarget)
        {
            OnInteract.AddListener(OnInteractCallback);

            _highlightTarget = highlightTarget;
        }
        public void Construct(UnityAction OnInteractCallback){
            OnInteract.AddListener(OnInteractCallback);
            _highlightTarget = null;
        }

        public void Interact()
        {
            OnInteract.Invoke();
        }

        public void Highlight()
        {
            _highlightTarget.color = Color.yellow;
        }
    }
}
