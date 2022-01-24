using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simmer.Items;
using Simmer.Inventory;

namespace Simmer.Player
{
    public class PlayerController : MonoBehaviour
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

        private bool _movementEnabled = false;

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
            primaryAction();
            secondaryAction();
            faceMouse();
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

        private void primaryAction()
        {
            Debug.DrawRay(transform.position, transform.right, Color.blue, 0, false);
            if(Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Player pressed F");
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1, 64);
                Collider2D obj = hit.collider;
                if(obj != null){
                    Debug.Log("Got an object:"+ obj);
                    if (hit.transform.gameObject.TryGetComponent(out GenericAppliance app))
                    {
                        OvenManager oven = (OvenManager)app;
                        oven.ToggleOn();
                    }else{
                        Debug.Log("get Component failed");
                    }
                }
            }
        }

        private void secondaryAction(){
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player pressed E");
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1, 64);
                Collider2D obj = hit.collider;
                if(obj != null){
                    Debug.Log("Got an object:"+ obj);
                    if (hit.transform.gameObject.TryGetComponent(out GenericAppliance app))
                    {
                        OvenManager oven = (OvenManager)app;
                        FoodItem selectedFoodItem = _playerManager
                            .playerInventory.GetSelectedItem();


                        if (selectedFoodItem.ingredientData
                            .applianceRecipeDict.ContainsKey(oven.applianceData))
                        {
                            print("Successfully added item: "
                                + selectedFoodItem.ingredientData + " to "
                                + oven.applianceData);

                            _playerInventory.RemoveFoodItem(
                            _playerInventory.selectedItemIndex);
                            oven.AddItem(selectedFoodItem);
                        }
                        else
                        {
                            print("Unsuccessfully added item: "
                                + selectedFoodItem.ingredientData + " to "
                                + oven.applianceData);
                        }
                    }
                    else
                    {
                        Debug.Log("get Component failed");
                    }
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