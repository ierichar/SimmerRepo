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
    [SerializeField] public GameObject menuCanvas;

    public void Construct(VN_Manager VNmanager)
    {
        vn_manager = VNmanager;
    }

    public void PlayGame()
    {
        //_sceneLoader.OnSceneLoad.Invoke(_kitchenScene);
        SceneManager.LoadScene("KitchenScene");
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
