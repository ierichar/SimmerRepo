using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simmer.Items;
using Simmer.Inventory;

namespace Simmer.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private PlayerInventory _playerInventory;
        private Rigidbody2D _rigidbody2D;

        [SerializeField] private float accelRate;
        [Range(0, 1)]
        [SerializeField] private float deccelRate;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float stopSpeed;
        private Vector2 _inputVector;
        private Vector2 _currentVelocity;

        [SerializeField] private bool _movementEnabled = false;

        public void Construct(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerInventory = _playerManager.playerInventory;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _movementEnabled = true;
        }

        private void Update()
        {
            GetMoveInput();
        }

        private void FixedUpdate()
        {
            if(_movementEnabled)
            {
                if (_inputVector == Vector2.zero)
                {
                    Decel();
                }
                else
                {
                    Move();
                }

                if (_currentVelocity.magnitude < stopSpeed)
                {
                    _currentVelocity = Vector2.zero;
                }

                _currentVelocity = Vector2.ClampMagnitude(_currentVelocity, maxSpeed);
                _rigidbody2D.velocity = _currentVelocity;
            }
            
        }

        private void GetMoveInput()
        {
            _inputVector = new Vector2(Input.GetAxis("Horizontal")
                , Input.GetAxis("Vertical"));
            _inputVector.Normalize();
        }

        private void Move()
        {
            _currentVelocity += _inputVector * accelRate;
        }

        private void Decel()
        {
            _currentVelocity *= deccelRate;
        }
    }
}