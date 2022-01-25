using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Player;
using Simmer.UI;

public class GameManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private PlayCanvasManager _playCanvasManager;

    private void Awake()
    {
        _playCanvasManager = FindObjectOfType<PlayCanvasManager>();
        _playCanvasManager.Construct();

        _playerManager = FindObjectOfType<PlayerManager>();
        _playerManager.Construct(_playCanvasManager.inventoryUIManager);
    }

    public void QuitGame()
    {
      Application.Quit();
    }
}
