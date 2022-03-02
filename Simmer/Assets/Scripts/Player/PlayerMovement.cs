using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        private AudioSource footstep;

        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;

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
        private int _cases;

        [Tooltip("True allows input to affect movement")]
        [SerializeField] private bool _movementEnabled = false;

        public void Construct(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            footstep = GetComponent<AudioSource>();
            SetMovementEnabled(true);
            _cases = 0;

            playerManager.gameEventManager
                .onInteractUI.AddListener(OnInteractUICallback);
        }
        

        private void Update()
        {
            if (!_movementEnabled)
            {
                StopMovement();
                return;
            }


            GetMoveInput();
            CheckAnimation();

        }

        private void FixedUpdate()
        {
            if (!_movementEnabled) return;

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

        private void OnInteractUICallback(bool isInteract)
        {
            // If true (beginning to interact) then stop movement
            SetMovementEnabled(!isInteract);
        }

        private void SetMovementEnabled(bool result)
        {
            if(!_movementEnabled) StopMovement();

            _movementEnabled = result;
        }

        private void StopMovement()
        {
            // Reset all movement vectors to zero
            _currentVelocity = Vector2.zero;
            _rigidbody2D.velocity = _currentVelocity;
            _inputVector = Vector2.zero;
            CheckAnimation();
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

        //Case 0: W pressed
        //Case 1: A pressed
        //Case 2: S pressed
        //Case 3: D pressed
        //Case 4: idle
        private void UpdateAnimator(int state){
            _cases = state;

            if(_animator.IsInTransition(0)){
                //print("Returning");
                return;
            }

            switch(_cases){
                case 0:
                    //_renderer.flipX = false;
                    _animator.SetBool("W", true);
                    _animator.SetBool("A", false);
                    _animator.SetBool("S", false);
                    _animator.SetBool("D", false);
                    makeStep();
                    break;
                case 1:
                    _renderer.flipX = true;
                    _animator.SetBool("W", false);
                    _animator.SetBool("A", true);
                    _animator.SetBool("S", false);
                    _animator.SetBool("D", false);
                    makeStep();
                    break;
                case 2:
                    //_renderer.flipX = false;
                    _animator.SetBool("W", false);
                    _animator.SetBool("A", false);
                    _animator.SetBool("S", true);
                    _animator.SetBool("D", false);
                    makeStep();
                    break;
                case 3:
                    _renderer.flipX = false;
                    _animator.SetBool("W", false);
                    _animator.SetBool("A", false);
                    _animator.SetBool("S", false);
                    _animator.SetBool("D", true);
                    makeStep();
                    break;
                case 4:
                    _animator.SetBool("W", false);
                    _animator.SetBool("A", false);
                    _animator.SetBool("S", false);
                    _animator.SetBool("D", false);
                    stopWalking();
                    break;
            }
        }
        private void CheckAnimation(){
            //const float lowestSpeed = 0.1f;
            //if(_inputVector.magnitude <= lowestSpeed)
            //{
            //    UpdateAnimator(4);
            //}
            //if (_inputVector.x > lowestSpeed)
            //{
            //    UpdateAnimator(3);
            //}
            //else if (_inputVector.x <= -lowestSpeed)
            //{
            //    UpdateAnimator(1);
            //}
            //else if (_inputVector.y <= -lowestSpeed)
            //{
            //    UpdateAnimator(2);
            //}
            //else if (_inputVector.y > lowestSpeed)
            //{
            //    UpdateAnimator(0);
            //}
        }

        private void makeStep()
        {
            //footstep.Play();
        }
        private void stopWalking()
        {
            //footstep.Stop();
        }

    }
}
