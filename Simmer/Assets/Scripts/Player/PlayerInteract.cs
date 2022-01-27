using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.Inventory;

namespace Simmer.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private PlayerInventory _playerInventory;

        public void Construct(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerInventory = playerManager.playerInventory;
        }

        public void Update()
        {
            primaryAction();
            secondaryAction();
            faceMouse();
        }

        private void primaryAction()
        {
            Debug.DrawRay(transform.position, transform.right, Color.blue, 0, false);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Player pressed F");
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1, 64);
                Collider2D obj = hit.collider;
                if (obj != null)
                {
                    if (hit.transform.gameObject.TryGetComponent(out GenericAppliance app))
                    {
                        FoodItem selected = _playerManager.playerInventory.GetSelectedItem();

                        if (selected != null && selected.ingredientData
                                .applianceRecipeDict.ContainsKey(app.applianceData))
                        {
                            print("Successfully added item: "
                                + selected.ingredientData + " to "
                                + app.applianceData);

                            _playerInventory.RemoveFoodItem(
                            _playerInventory.selectedItemIndex);
                        }

                        app.TryInteract(selected);
                    }
                    else
                    {
                        Debug.Log("get Component failed");
                    }
                }
            }
        }

        private void secondaryAction()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player pressed E");
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1, 64);
                Collider2D obj = hit.collider;
                if (obj != null)
                {
                    Debug.Log("Got an object:" + obj);
                    if (hit.transform.gameObject.TryGetComponent(out GenericAppliance app))
                    {
                        FoodItem selectedFoodItem = _playerManager
                            .playerInventory.GetSelectedItem();

                        //if(selectedFoodItem!=null)
                        //{
                        //    if (selectedFoodItem.ingredientData
                        //        .applianceRecipeDict.ContainsKey(app.applianceData))
                        //    {
                        //        print("Successfully added item: "
                        //            + selectedFoodItem.ingredientData + " to "
                        //            + app.applianceData);

                        //        _playerInventory.RemoveFoodItem(
                        //        _playerInventory.selectedItemIndex);
                        //        app.AddItem(selectedFoodItem);
                        //    }
                        //    else
                        //    {
                        //        print("Unsuccessfully added item: "
                        //            + selectedFoodItem.ingredientData + " to "
                        //            + app.applianceData);
                        //    }
                        //}
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