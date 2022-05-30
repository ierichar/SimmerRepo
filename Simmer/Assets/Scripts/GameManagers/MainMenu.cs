using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

using Ink.Runtime;

using Simmer.Interactable;
using Simmer.VN;
using Simmer.UI;

public class MainMenu : MonoBehaviour
{
    public VN_Manager vn_manager { get; private set; }
    [SerializeField] private TextAsset npcInkAsset;
    [SerializeField] private TextAsset npcInkAssetIntro;
    [SerializeField] public GameObject menuCanvas;
    private static bool isFirstLoad = true;

    public void Construct(VN_Manager VNmanager)
    {
        vn_manager = VNmanager;
    }

    public void PlayGame(TextAsset npcInkAssetIntro)
    {
        // Introduction to game
        if (isFirstLoad)
        {
            vn_manager.inkJSONAsset = npcInkAssetIntro;
            vn_manager.StartStory();
            vn_manager.OnEndStory.AddListener(BackToMenu);
            isFirstLoad = false;
            Debug.Log("Starting ink script");
        } else 
        {
            SceneManager.LoadScene("KitchenScene");
        }
        //_sceneLoader.OnSceneLoad.Invoke(_kitchenScene);
        //SceneManager.LoadScene("KitchenScene");
    }

    public void PlayTutorial(TextAsset npcInkAsset)
    {
        vn_manager.inkJSONAsset = npcInkAsset;
        vn_manager.StartStory();
        vn_manager.OnEndStory.AddListener(BackToMenu);
    }

    private void BackToMenu(){
        menuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
