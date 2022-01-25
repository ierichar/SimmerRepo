using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Inventory;

namespace Simmer.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public GameEventManager gameEventManager { get; private set; }
        public InventoryUIManager inventoryUIManager { get; private set; }

        public PlayerEventManager playerEventManager { get; private set; }
        public PlayerController playerController { get; private set; }
        public PlayerInventory playerInventory { get; private set; }
        public PlayerItemSelect playerItemSelect { get; private set; }
        public PlayerHeldItem playerHeldItem { get; private set; }

        public void Construct(GameEventManager gameEventManager
            , InventoryUIManager inventoryUIManager)
        {
            this.gameEventManager = gameEventManager;
            this.inventoryUIManager = inventoryUIManager;

            playerEventManager = GetComponent<PlayerEventManager>();
            playerController = GetComponent<PlayerController>();
            playerInventory = GetComponent<PlayerInventory>();
            playerHeldItem = GetComponentInChildren<PlayerHeldItem>();
            playerItemSelect = GetComponent<PlayerItemSelect>();

            playerEventManager.Construct(this);
            playerController.Construct(this);
            playerInventory.Construct(this);
            playerHeldItem.Construct(this);
            playerItemSelect.Construct(this);
        }

    }
}
