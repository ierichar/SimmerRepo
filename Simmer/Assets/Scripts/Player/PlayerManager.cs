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
        public PlayerMovement playerMovement { get; private set; }
        public PlayerInventory playerInventory { get; private set; }
        public PlayerItemSelect playerItemSelect { get; private set; }
        public PlayerHeldItem playerHeldItem { get; private set; }
        public PlayerRayInteract playerInteract { get; private set; }

        public void Construct(GameEventManager gameEventManager
            , InventoryUIManager inventoryUIManager)
        {
            this.gameEventManager = gameEventManager;
            this.inventoryUIManager = inventoryUIManager;

            playerEventManager = GetComponent<PlayerEventManager>();
            playerMovement = GetComponent<PlayerMovement>();
            playerInventory = GetComponent<PlayerInventory>();
            playerItemSelect = GetComponent<PlayerItemSelect>();
            playerHeldItem = GetComponentInChildren<PlayerHeldItem>();
            playerInteract = GetComponentInChildren<PlayerRayInteract>();

            playerEventManager.Construct(this);
            playerMovement.Construct(this);
            playerInventory.Construct(this);
            playerItemSelect.Construct(this);
            playerHeldItem.Construct(this);
            playerInteract.Construct(this);
        }

    }
}
