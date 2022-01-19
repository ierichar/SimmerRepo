using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.TopdownPlayer
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        [SerializeField] private float accelRate;
        [SerializeField] private float deccelRate;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float stopSpeed;

        private Vector2 _inputVector;
        private Vector2 _currentVelocity;

        public void Construct()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            GetMoveInput();
        }

        private void FixedUpdate()
        {
            if (_inputVector == Vector2.zero)
            {
                Decel();
            }
            else
            {
                Move();
            }

            if (_currentVelocity.magnitude < maxSpeed)
            {
                _currentVelocity = Vector2.zero;
            }

            _currentVelocity = Vector2.ClampMagnitude(_currentVelocity, maxSpeed);
            _rigidbody2D.velocity = _currentVelocity;
        }

        private void GetMoveInput()
        {
            _inputVector = new Vector2(Input.GetAxis("Horizontal")
                , Input.GetAxis("Vertical"));
            _inputVector.Normalize();
            print("_inputVector: " + _inputVector);
        }

        private void Move()
        {
            _currentVelocity += _inputVector * accelRate;
        }

        private void Decel()
        {
            _currentVelocity -= _currentVelocity.normalized * deccelRate;
        }
    }
}