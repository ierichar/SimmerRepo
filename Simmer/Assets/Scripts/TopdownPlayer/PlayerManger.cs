using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.TopdownPlayer
{
    public class PlayerManger : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerController.Construct();
        }
    }

}
