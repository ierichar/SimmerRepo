using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Player;
using Simmer.UI;
using Simmer.SceneManagement;

public class MarketGameManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private PlayCanvasManager _marketCanvasManager;
    private GameEventManager _gameEventManager;

    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _marketCanvasManager = FindObjectOfType<MarketCanvasManager>();
        _gameEventManager = GetComponent<GameEventManager>();
        _sceneLoader = GetComponent<SceneLoader>();

        _gameEventManager.Construct();
        _marketCanvasManager.Construct(_gameEventManager.OnSelectItem);
        _playerManager.Construct(_gameEventManager, _marketCanvasManager.inventoryUIManager);
        _sceneLoader.Construct(_marketCanvasManager);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
