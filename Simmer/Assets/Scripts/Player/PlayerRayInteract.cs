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

        private bool _isInvOpen;

        public UnityEvent OnCloseUIWindow;

        [SerializeField] private float _interactDistance;

        private InteractableBehaviour _previousSelected;
        private InteractableBehaviour _currentSelected;

        public void Construct(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerInventory = playerManager.playerInventory;

            OnCloseUIWindow = playerManager.gameEventManager
                .OnCloseUIWindow;

            OnCloseUIWindow.AddListener(CloseInv);

            _isInvOpen = false;
        }

        public void Update()
        {
            primaryAction();
            faceMouse();
        }

        private void primaryAction()
        {
            Debug.DrawRay(transform.position, transform.right
                , Color.blue, 0, false);

            RaycastHit2D hit = Physics2D.Raycast(transform.position
                , transform.right, _interactDistance, 64);

            Collider2D obj = hit.collider;
            //if (obj != null)
            //{
            //    //store gameObject of collider that was hit with raycast
            //    _currentlyOpen = hit.transform.gameObject;
            //    // TESTING INTERABLEBEHAVIOUR
            //    if (_currentlyOpen.TryGetComponent(
            //        out InteractableBehaviour interactable))
            //    {
            //        interactable.StartHighlight();
            //        _currentSelected = interactable;
            //    }
            //}

            if (obj != null && hit.transform.gameObject.TryGetComponent(
                out InteractableBehaviour interactable))
            {
                if (_currentSelected != null) _currentSelected.StopHighlight();
                interactable.StartHighlight();
                _currentSelected = interactable;
                
            }
            else
            {
                if(_currentSelected != null) _currentSelected.StopHighlight();
                _currentSelected = null;
            }

            if (Input.GetMouseButtonDown(1) && !_isInvOpen)
            {
                if (_currentSelected != null)
                {
                    _previousSelected = _currentSelected;
                    _isInvOpen = true;
                    _previousSelected.Interact();
                }
            }
            else if (Input.GetMouseButtonDown(1) && _isInvOpen)
            {
                if (_previousSelected != null)
                {
                    _isInvOpen = false;
                    _previousSelected.Interact();
                    _previousSelected = null;
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

        public void CloseInv(){
            //if(!_isInvOpen) return false;

            _isInvOpen = false;
            //return true;
        }
    }
}