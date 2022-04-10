using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;

using Simmer.UI;
using Simmer.Player;
using Simmer.Inventory;

namespace Simmer.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        private ScreenBlockManager _screenBlockManager;
        private PlayerInventory _playerInventory;
        public UnityEvent<SceneData> OnSceneLoad = new UnityEvent<SceneData>();

        private List<ExitDoorBehaviour> exitDoorList = new List<ExitDoorBehaviour>();

        [SerializeField] private float _fadeTime;
        [SerializeField] private float _loadDelay;
        [SerializeField] private Ease _fadeEase;

        [SerializeField] private bool _fadeInOnLoad;

        private bool isSceneLoading = false;

        public void Construct(PlayerManager playerManager
            , PlayCanvasManager playCanvasManager)
        {
            OnSceneLoad.AddListener(OnSceneLoadCallback);
            _playerInventory = playerManager.playerInventory;
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

            GlobalPlayerData.SaveInventoryDictionary
                (_playerInventory.inventoryItemDictionary);

            foreach(GenericAppliance app in FindObjectsOfType<GenericAppliance>()){
                app.SaveInventory();
            }
            PantrySlotGroupManager obj = FindObjectOfType<PantrySlotGroupManager>(true);
            if(obj!=null)
                obj.SaveInventory();

            Tween fadeTween = _screenBlockManager
                .Fade(1, _fadeTime, _fadeEase);
            yield return fadeTween.WaitForCompletion();

            yield return new WaitForSeconds(_loadDelay);
            SceneManager.LoadScene(sceneData.sceneName);
        }
    }
}