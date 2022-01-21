using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Inventory;

namespace Simmer.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerController _playerController;
        private PlayerInventory _playerInventory;

        public void Construct(InventoryManager _inventoryManager)
        {
            _playerController = GetComponent<PlayerController>();
            _playerController.Construct();

            _playerInventory = GetComponent<PlayerInventory>();
            _playerInventory.Construct(_inventoryManager);
        }

    }

}
