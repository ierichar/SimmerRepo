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

    private void Awake()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _kitchenCanvasManager = FindObjectOfType<KitchenCanvasManager>();
        _gameEventManager = GetComponent<GameEventManager>();
        _sceneLoader = GetComponent<SceneLoader>();

        _gameEventManager.Construct();
        _kitchenCanvasManager.Construct(_gameEventManager.OnSelectItem);
        _playerManager.Construct(_gameEventManager, _kitchenCanvasManager.inventoryUIManager);
        _sceneLoader.Construct(_playerManager, _kitchenCanvasManager);
    }

    public void QuitGame()
    {
      Application.Quit();
    }
}
