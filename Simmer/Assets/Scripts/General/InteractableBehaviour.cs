using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Simmer.Interactable
{
    /// <summary>
    /// Defines the functionality for player interaction on this GameObject.
    /// A parent script should Construct this with appropriate paramaters.
    /// </summary>
    public class InteractableBehaviour : MonoBehaviour
    {
        public UnityEvent OnInteract = new UnityEvent();

        private SpriteRendererManager _highlightTarget;

        /// <summary>
        /// True to allow player to stop interaction with
        /// PlayerRayInteract. If false, stop interaction by calling
        /// GameEventManager.onInteractUI.Invoke(false) like in
        /// NPC_Manager.StopInteractSequence().
        /// </summary>
        public bool isInteractToggle;// { get; private set; }

        /// <summary>
        /// Constructs this to interface with the parent class
        /// of this interactable GameObject
        /// </summary>
        /// <param name="OnInteractCallback">
        /// The function to call when the player interacts with this
        /// </param>
        /// <param name="highlightTarget">
        /// Sprite to modify when interacting
        /// </param>
        /// <param name="isInteractToggle">
        /// Sets InteractableBehaviour member field of same name.
        /// </param>
        public void Construct(UnityAction OnInteractCallback
            , SpriteRendererManager highlightTarget
            , bool isInteractToggle)
        {
            OnInteract.AddListener(OnInteractCallback);

            _highlightTarget = highlightTarget;
            StopHighlight();

            this.isInteractToggle = isInteractToggle;
        }

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
