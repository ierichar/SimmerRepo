using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Player;
using Simmer.UI;

public class GameManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private PlayCanvasManager _playCanvasManager;
    private GameEventManager _gameEventManager;

    private void Awake()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _playCanvasManager = FindObjectOfType<PlayCanvasManager>();
        _gameEventManager = GetComponent<GameEventManager>();

        _gameEventManager.Construct();
        _playCanvasManager.Construct(_gameEventManager.OnSelectItem);
        _playerManager.Construct(_gameEventManager, _playCanvasManager.inventoryUIManager);
    }
}
