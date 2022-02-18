using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Inventory;
using Simmer.Items;
using Simmer.UI.Tooltips;
using Simmer.UI.RecipeMap;

namespace Simmer.UI
{
    public class KitchenCanvasManager : PlayCanvasManager
    {
        public override void Construct(UnityEvent<int> OnSelectItem, UISoundManager soundManager)
        {
            base.Construct(OnSelectItem, soundManager);

            // TEMP TO TEST ITEMSLOTMANAGER
            PantryUI pantryUI
                = FindObjectOfType<PantryUI>();
            if (pantryUI)
            {
                pantryUI.Construct(itemFactory);
            }


            GenericAppliance[] applianceManager = FindObjectsOfType<GenericAppliance>();
            foreach(GenericAppliance appliance in applianceManager){
                appliance.Construct(itemFactory, soundManager);
            }
        }
    }
}