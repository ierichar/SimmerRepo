using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Player;
using Simmer.UI;
using Simmer.VN;
using Simmer.Tutorial;

public class MenuManager : MonoBehaviour
{
   
    private GameEventManager _gameEventManager;
    private VN_Manager _VN_Manager;
    private MainMenu _MainMenu;
    private TutorialManager _tutorialManager;

    private void Awake()
    {
        _VN_Manager = FindObjectOfType<VN_Manager>();
        _gameEventManager = GetComponent<GameEventManager>();
        _MainMenu = FindObjectOfType<MainMenu>();
        _tutorialManager = FindObjectOfType<TutorialManager>();

        _gameEventManager.Construct();
        _VN_Manager.Construct();
        _MainMenu.Construct(_VN_Manager);
        _tutorialManager.Construct();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
