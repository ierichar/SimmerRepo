using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.TopdownPlayer
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        [SerializeField] private float accelRate;
        [Range(0, 1)]
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
            RaycastInteract();
            faceMouse();
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

            if (_currentVelocity.magnitude < stopSpeed)
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
        }

        private void Move()
        {
            _currentVelocity += _inputVector * accelRate;
        }

        private void Decel()
        {
            _currentVelocity *= deccelRate;
        }

        private void RaycastInteract()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.right, Color.blue, 0, false);

            if (Physics.Raycast(transform.position, transform.right, out hit, 5, 1))
            {
                if (hit.transform.gameObject.TryGetComponent(out GenericAppliance app))
                {
                    OvenManager oven = (OvenManager)app;
                    oven.ToggleOn();
                }
            }
            
        }
        private void faceMouse() {
            var mouseDir = Input.mousePosition - 
            Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}