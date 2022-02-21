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

    [SerializeField] private SaveData _startSaveData;

    private void Awake()
    {
        GlobalPlayerData.Construct(_startSaveData);

        _playerManager = FindObjectOfType<PlayerManager>();
        _marketCanvasManager = FindObjectOfType<MarketCanvasManager>();
        _NPC_Manager = FindObjectOfType<NPC_Manager>();
        _VN_Manager = FindObjectOfType<VN_Manager>();
        _gameEventManager = GetComponent<GameEventManager>();
        _sceneLoader = GetComponent<SceneLoader>();
        _soundManager = FindObjectOfType<UISoundManager>();

        _gameEventManager.Construct();
        _marketCanvasManager.Construct(_gameEventManager.OnSelectItem, _soundManager);
        _playerManager.Construct(_gameEventManager, _marketCanvasManager);
        _sceneLoader.Construct(_playerManager, _marketCanvasManager);

        _VN_Manager.Construct();
        _NPC_Manager.Construct(_VN_Manager
            , _marketCanvasManager);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
