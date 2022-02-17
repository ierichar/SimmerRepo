using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Simmer.Interactable;
using UnityEngine.Events;
using Simmer.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*
    [SerializeField] private SceneData _kitchenScene;
    [SerializeField] private SceneLoader _sceneLoader;
    //public void Awake(){
    //    _sceneLoader = GetComponent<SceneLoader>();
    //}
    */
    public void PlayGame()
    {
        //_sceneLoader.OnSceneLoad.Invoke(_kitchenScene);
        SceneManager.LoadScene("KitchenScene");
    }
}
