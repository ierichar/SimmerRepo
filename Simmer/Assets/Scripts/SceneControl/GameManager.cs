using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Player;
using Simmer.Inventory;

public class GameManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private InventoryUIManager _inventoryManager;

    private void Awake()
    {
        _inventoryManager = FindObjectOfType<InventoryUIManager>();
        _inventoryManager.Construct();

        _playerManager = FindObjectOfType<PlayerManager>();
        _playerManager.Construct(_inventoryManager);
    }
}
