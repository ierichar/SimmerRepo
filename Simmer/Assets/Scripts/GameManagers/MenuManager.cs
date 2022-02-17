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
    private NPC_Manager _NPC_Manager;
    private VN_Manager _VN_Manager;


    private void Awake()
    {
        _NPC_Manager = FindObjectOfType<NPC_Manager>();
        _VN_Manager = FindObjectOfType<VN_Manager>();
        _gameEventManager = GetComponent<GameEventManager>();

        _gameEventManager.Construct();
        _VN_Manager.Construct();
        _NPC_Manager.Construct(_VN_Manager
            , null);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
