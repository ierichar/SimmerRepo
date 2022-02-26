using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Simmer.Interactable;
using UnityEngine.Events;

namespace Simmer.SceneManagement
{
    public class ExitDoorBehaviour : MonoBehaviour
    {
        [SerializeField] private SceneData sceneToLoad;
        private InteractableBehaviour _interactableBehaviour;
        private SpriteRendererManager _highlightSprite;

        public UnityEvent<SceneData> OnSceneLoad = new UnityEvent<SceneData>();

        public void Construct()
        {
            _highlightSprite = GetComponentInChildren<SpriteRendererManager>();
            _highlightSprite.Construct();

            _interactableBehaviour = GetComponent<InteractableBehaviour>();
            _interactableBehaviour.Construct(
                OnInteractCallback, _highlightSprite, false);
        }

        private void OnInteractCallback()
        {
            OnSceneLoad.Invoke(sceneToLoad);
        }
    }
}