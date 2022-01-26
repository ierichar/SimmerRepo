using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Simmer.Player
{
    public class PlayerEventManager : MonoBehaviour
    {
        public UnityEvent<int> OnSelectInventoryItem = new UnityEvent<int>();

        public UnityEvent OnDropItem = new UnityEvent();

        public UnityEvent OnAddRandomItem = new UnityEvent();

        public void Construct(PlayerManager playerManager)
        {

        }
    }
}