using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Player;
using Simmer.UI;
using Simmer.VN;
using Simmer.NPC;
using Simmer.SceneManagement;

public class MarketGameManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private MarketCanvasManager _marketCanvasManager;
    private GameEventManager _gameEventManager;
    private NPC_Manager _NPC_Manager;
    private VN_Manager _VN_Manager;

    private SceneLoader _sceneLoader;
    private UISoundManager _soundManager;

    private PauseMenu _pauseMenu;

    [SerializeField] private SaveData _startSaveData;

    private void Awake()
    {
        GlobalPlayerData.Construct(_startSaveData);

        _marketCanvasManager = FindObjectOfType<MarketCanvasManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _NPC_Manager = FindObjectOfType<NPC_Manager>();
        _VN_Manager = FindObjectOfType<VN_Manager>();
        _gameEventManager = GetComponent<GameEventManager>();
        _sceneLoader = GetComponent<SceneLoader>();
        _soundManager = FindObjectOfType<UISoundManager>();
        _pauseMenu = FindObjectOfType<PauseMenu>();

        _gameEventManager.Construct();
        _marketCanvasManager.Construct(_gameEventManager, _soundManager);
        _playerManager.Construct(_gameEventManager, _marketCanvasManager);
        _sceneLoader.Construct(_playerManager, _marketCanvasManager);
        _pauseMenu.Construct(_sceneLoader);

        _VN_Manager.Construct();
        _NPC_Manager.Construct(_VN_Manager
            , _marketCanvasManager, _gameEventManager);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
