using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Simmer.Player
{
    /// <summary>
    /// Currently holds events for debug actions:
    /// destroy selected item and add random item
    /// </summary>
    public class PlayerEventManager : MonoBehaviour
    {
        /// <summary>
        /// Event for destroying selected player item
        /// Declared in PlayerEventManager, invoked in PlayerItemSelect,
        /// listeners in PlayerInventory
        /// </summary>
        public UnityEvent OnDropItem = new UnityEvent();

        /// <summary>
        /// Event for adding random item to nearest empty inventory slot
        /// Declared in PlayerEventManager, invoked in PlayerItemSelect,
        /// listeners in PlayerInventory
        /// </summary>
        public UnityEvent OnAddRandomItem = new UnityEvent();

        public void Construct(PlayerManager playerManager)
        {

        }
    }
}