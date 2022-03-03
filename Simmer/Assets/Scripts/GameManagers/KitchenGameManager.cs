using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Player;
using Simmer.UI;
using Simmer.SceneManagement;

/// <summary>
/// 
/// </summary>
public class KitchenGameManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private KitchenCanvasManager _kitchenCanvasManager;
    private GameEventManager _gameEventManager;

    private SceneLoader _sceneLoader;
    private PauseMenu _pauseMenu;
    public UISoundManager soundManager{get; private set;}

    [SerializeField] private SaveData _startSaveData;

    private void Awake()
    {
        GlobalPlayerData.Construct(_startSaveData);

        _playerManager = FindObjectOfType<PlayerManager>();
        _kitchenCanvasManager = FindObjectOfType<KitchenCanvasManager>();
        _gameEventManager = GetComponent<GameEventManager>();
        _sceneLoader = GetComponent<SceneLoader>();
        _pauseMenu = GetComponent<PauseMenu>();
        soundManager = FindObjectOfType<UISoundManager>();

        _gameEventManager.Construct();
        _kitchenCanvasManager.Construct(_gameEventManager, soundManager);
        _playerManager.Construct(_gameEventManager, _kitchenCanvasManager);
        _sceneLoader.Construct(_playerManager, _kitchenCanvasManager);
        _pauseMenu.Construct(_sceneLoader);
    }
}
