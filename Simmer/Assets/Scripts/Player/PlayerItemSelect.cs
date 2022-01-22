using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Player;

namespace Simmer.Inventory
{
    public class PlayerItemSelect : MonoBehaviour
    {
        private PlayerEventManager _playerEventManager;

        private bool selectionEnabled = false;

        public void Construct(PlayerManager playerManager)
        {
            _playerEventManager = playerManager.playerEventManager;
            selectionEnabled = true;
        }

        private void Update()
        {
            if(selectionEnabled)
            {
                if (Input.GetButtonDown("Drop"))
                {
                    _playerEventManager.OnDropItem.Invoke();
                }
                if (Input.GetButtonDown("Interact"))
                {
                    _playerEventManager.OnAddRandomItem.Invoke();
                }

                if (Input.GetButtonDown("HotbarSelect0"))
                {
                    _playerEventManager.OnSelectItem.Invoke(0);
                }
                if (Input.GetButtonDown("HotbarSelect1"))
                {
                    _playerEventManager.OnSelectItem.Invoke(1);
                }
                if (Input.GetButtonDown("HotbarSelect2"))
                {
                    _playerEventManager.OnSelectItem.Invoke(2);
                }
                if (Input.GetButtonDown("HotbarSelect3"))
                {
                    _playerEventManager.OnSelectItem.Invoke(3);
                }
            }
        }
    }
}