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

        [SerializeField] private float _interactDistance;

        private InteractableBehaviour _previousInteracted;
        private InteractableBehaviour _currentSelected;

        public void Construct(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerInventory = playerManager.playerInventory;
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

            if (Input.GetMouseButtonDown(1))
            {
                // Stop interact
                if (_previousInteracted != null)
                {
                    _previousInteracted.Interact();
                    _previousInteracted = null;
                }
                // New interact
                else if (_currentSelected != null)
                {
                    _previousInteracted = _currentSelected;
                    _previousInteracted.Interact();
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