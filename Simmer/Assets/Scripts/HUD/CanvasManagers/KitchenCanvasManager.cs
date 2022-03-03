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
        public GenericAppliance[] applianceManager{ get; protected set;}
        public override void Construct(
            GameEventManager gameEventManager
            , UISoundManager soundManager)
        {
            base.Construct(gameEventManager, soundManager);

            // TEMP TO TEST ITEMSLOTMANAGER
            PantryUI pantryUI
                = FindObjectOfType<PantryUI>();
            if (pantryUI)
            {
                pantryUI.Construct(itemFactory);
            }

            applianceManager = FindObjectsOfType<GenericAppliance>();
            foreach(GenericAppliance appliance in applianceManager){
                appliance.Construct(itemFactory, soundManager);
            }
            SplitStoveOven spliter = FindObjectOfType<SplitStoveOven>();
            spliter.Construct();
        }
    }
}