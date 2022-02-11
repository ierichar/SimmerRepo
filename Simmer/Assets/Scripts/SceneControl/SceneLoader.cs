using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using DG.Tweening;

using Simmer.UI;

namespace Simmer.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        private ScreenBlockManager _screenBlockManager;

        private List<ExitDoorBehaviour> exitDoorList = new List<ExitDoorBehaviour>();

        [SerializeField] private float _fadeTime;
        [SerializeField] private float _loadDelay;
        [SerializeField] private Ease _fadeEase;

        [SerializeField] private bool _fadeInOnLoad;

        private bool isSceneLoading = false;

        public void Construct(PlayCanvasManager playCanvasManager)
        {
            _screenBlockManager = playCanvasManager.screenBlockManager;

            ExitDoorBehaviour[] sceneChangeArray = FindObjectsOfType<ExitDoorBehaviour>();
            foreach(ExitDoorBehaviour exitDoor in sceneChangeArray)
            {
                exitDoorList.Add(exitDoor);
                exitDoor.Construct();

                exitDoor.OnSceneLoad.AddListener(OnSceneLoadCallback);
            }

            if(_fadeInOnLoad)
            {
                _screenBlockManager.SetColor(Color.black);
                _screenBlockManager.Fade(0, _fadeTime, _fadeEase);
            }
        }

        private void OnSceneLoadCallback(SceneData sceneData)
        {
            if(!isSceneLoading)
            {
                StartCoroutine(LoadSceneSequence(sceneData));
            }
        }

        private IEnumerator LoadSceneSequence(SceneData sceneData)
        {
            isSceneLoading = true;
            Tween fadeTween = _screenBlockManager
                .Fade(1, _fadeTime, _fadeEase);
            yield return fadeTween.WaitForCompletion();

            yield return new WaitForSeconds(_loadDelay);
            SceneManager.LoadScene(sceneData.sceneName);
        }
    }
}