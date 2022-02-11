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

        public UnityEvent<SceneData> OnSceneLoad = new UnityEvent<SceneData>();

        public void Construct()
        {
            _interactableBehaviour = GetComponent<InteractableBehaviour>();
            _interactableBehaviour.Construct(OnInteractCallback, null);
        }

        private void OnInteractCallback()
        {
            OnSceneLoad.Invoke(sceneToLoad);
        }
    }
}