using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Simmer.Player
{
    public class PlayerEventManager : MonoBehaviour
    {
        public UnityEvent<int> OnSelectItem = new UnityEvent<int>();

        public void Construct(PlayerManager playerManager)
        {

        }
    }
}