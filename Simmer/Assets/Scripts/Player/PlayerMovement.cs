using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simmer.Items;
using Simmer.Inventory;

namespace Simmer.Player
{
    /// <summary>
    /// Handles axis input and moves the player rigidbody
    /// Uses acceleration based movement
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private Rigidbody2D _rigidbody2D;

        [Tooltip("Rate of increase in velocity in direction" +
            " of input per FixedUpdate")]
        [SerializeField] private float accelRate;
        [Range(0, 1)]
        [Tooltip("If input is 0, current velocity is multiplied by this number")]
        [SerializeField] private float deccelRate;
        [Tooltip("Velocity magnitude is clamped by this number")]
        [SerializeField] private float maxSpeed;
        [Tooltip("If speed is below this number, velocity is set to 0")]
        [SerializeField] private float stopSpeed;

        /// <summary>
        /// Normalized input from horiztonal and vertical axes from Input Manager
        /// </summary>
        private Vector2 _inputVector;
        /// <summary>
        /// Current rigidbody velocity
        /// </summary>
        private Vector2 _currentVelocity;

        [Tooltip("True allows input to affect movement")]
        [SerializeField] private bool _movementEnabled = false;

        public void Construct(PlayerManager playerManager)
        {
            _playerManager = playerManager;
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