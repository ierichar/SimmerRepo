using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Player;

namespace Simmer.Inventory
{
    public class PlayerItemSelect : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private PlayerEventManager _playerEventManager;

        private bool selectionEnabled = false;

        public void Construct(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerEventManager = playerManager.playerEventManager;
            selectionEnabled = true;
        }

        private void Update()
        {
            if(selectionEnabled)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    _playerEventManager.OnDropItem.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    _playerEventManager.OnAddRandomItem.Invoke();
                }

                if (Input.GetButtonDown("HotbarSelect0"))
                {
                    _playerManager.gameEventManager.OnSelectItem.Invoke(0);
                }
                if (Input.GetButtonDown("HotbarSelect1"))
                {
                    _playerManager.gameEventManager.OnSelectItem.Invoke(1);
                }
                if (Input.GetButtonDown("HotbarSelect2"))
                {
                    _playerManager.gameEventManager.OnSelectItem.Invoke(2);
                }
                if (Input.GetButtonDown("HotbarSelect3"))
                {
                    _playerManager.gameEventManager.OnSelectItem.Invoke(3);
                }
            }
        }
    }
}