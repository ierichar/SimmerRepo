using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        public UnityEvent OnCloseInv = new UnityEvent();

        public void Construct(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerInventory = playerManager.playerInventory;
            OnCloseInv.AddListener(CloseInv);

            _isInvOpen = false;
        }

        public void Update()
        {
            primaryAction();
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
                        interactable.Interact();
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

        private void faceMouse()
        {
            var mouseDir = Input.mousePosition -
            Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void CloseInv(){
            //if(!_isInvOpen) return false;

            _isInvOpen = false;
            _currentlyOpen = null;
            //return true;
        }
    }
}