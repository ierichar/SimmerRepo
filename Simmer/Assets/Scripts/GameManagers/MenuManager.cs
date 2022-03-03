using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Player;
using Simmer.UI;
using Simmer.VN;
using Simmer.NPC;
using Simmer.SceneManagement;

public class MenuManager : MonoBehaviour
{
   
    private GameEventManager _gameEventManager;
    private VN_Manager _VN_Manager;
    private MainMenu _MainMenu;


    private void Awake()
    {
        _VN_Manager = FindObjectOfType<VN_Manager>();
        _gameEventManager = GetComponent<GameEventManager>();
        _MainMenu = FindObjectOfType<MainMenu>();

        _gameEventManager.Construct();
        _VN_Manager.Construct();
        _MainMenu.Construct(_VN_Manager);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
