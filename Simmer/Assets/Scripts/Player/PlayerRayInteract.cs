using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.Inventory;
using Simmer.Interactable;

namespace Simmer.Player
{
    public class PlayerRayInteract : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private PlayerInventory _playerInventory;

        private GameObject _currentlyOpen;
        private bool _isInvOpen;

        public void Construct(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerInventory = playerManager.playerInventory;
            _isInvOpen = false;
        }

        public void Update()
        {
            primaryAction();
            //secondaryAction();
            faceMouse();
        }

        private void primaryAction()
        {
            Debug.DrawRay(transform.position, transform.right, Color.blue, 0, false);

            
            if (Input.GetMouseButtonDown(1) && !_isInvOpen)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1.5f, 64);
                Collider2D obj = hit.collider;
                if (obj != null)
                {
                    //store gameObject of collider that was hit with raycast
                    _currentlyOpen = hit.transform.gameObject;
                    // TESTING INTERABLEBEHAVIOUR
                    if (_currentlyOpen.TryGetComponent(
                        out InteractableBehaviour interactable))
                    {
                        _isInvOpen = true;
                        //if (Input.GetKeyDown(KeyCode.F))
                        //{
                        //interactable.Highlight();
                        interactable.Interact();
                        //}
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1) && _isInvOpen)
            {
                if (_currentlyOpen.TryGetComponent(
                    out InteractableBehaviour interactable))
                {
                    _isInvOpen = false;
                    interactable.Interact();
                }
            }

        }

        private void secondaryAction()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("Player pressed E");
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1.5f, 64);
                Collider2D obj = hit.collider;
                //Debug.Log("Testing");
                if (obj != null)
                {
                    //Debug.Log("Got an object:" + obj);
                    if (hit.transform.gameObject.TryGetComponent(out GenericAppliance app))
                    {
                        app.ToggleInventory();
                    }
                    else
                    {
                        Debug.Log("get Component failed");
                    }
                }
            }
        }

        private void faceMouse()
        {
            var mouseDir = Input.mousePosition -
            Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}