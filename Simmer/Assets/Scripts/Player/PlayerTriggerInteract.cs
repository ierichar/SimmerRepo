using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.Inventory;

namespace Simmer.Player
{
    public class PlayerTriggerInteract : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private PlayerInventory _playerInventory;

        [SerializeField] private LayerMask applianceLayerMask;

        public void Construct(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerInventory = playerManager.playerInventory;

            print(this + " Construct");
        }

        public void Update()
        {
            transform.position = _playerManager.transform.position;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other != null
                && ((1 << other.gameObject.layer) & applianceLayerMask) != 0)
            {
                print("Found " + other.gameObject);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    print("F");
                    if (other.gameObject.TryGetComponent(out GenericAppliance app))
                    {
                        FoodItem selected = _playerManager.playerInventory.GetSelectedItem();

                        if (selected != null && selected.ingredientData
                                .applianceRecipeListDict.ContainsKey(app.applianceData))
                        {
                            print("Successfully added item: "
                                + selected.ingredientData + " to "
                                + app.applianceData);

                            _playerInventory.RemoveFoodItem(
                            _playerInventory.selectedItemIndex);
                        }
                        //BELOW LINE MAY BE NECCESSARY TO ADD BACK IN
                        //app.TryInteract(selected);
                    }
                    else
                    {
                        print("Couldn't get GenericAppliance");
                    }
                }
            }
        }
    }
}