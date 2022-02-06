using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Inventory;

namespace Simmer.Player
{
    /// <summary>
    /// Find and Constructs all scripts on the Player GameObject
    /// and holds public getters for them
    /// </summary>
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

        /// <summary>
        /// Constructs from the KitchenGameManager
        /// </summary>
        /// <param name="gameEventManager">
        /// Needed by playerItemSelect to invoke events
        /// </param>
        /// <param name="inventoryUIManager">
        /// Needed by playerInventory to reference inventory slots
        /// </param>
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
