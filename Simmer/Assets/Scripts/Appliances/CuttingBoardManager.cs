using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.Interactable;

public class CuttingBoardManager : GenericAppliance
{
    private bool cuttingStarted;
    private int numCuts;

    [SerializeField] private int numCutsMultiplier;

    public override void Construct(ItemFactory itemFactory){
        print("Calling derived class constructor from cutting board");
        base.Construct(itemFactory);

        //derived class variable init
        cuttingStarted = false;
        numCuts = 0;
    }

    public void chopping(){
        if(!cuttingStarted){
            Validation();

            if(_pendingTargetRecipe == null) 
                return;
            else 
                cuttingStarted = true;
        }else{
            if(numCuts >= _pendingTargetRecipe.baseActionTime * numCutsMultiplier){
                foreach(ItemSlotManager slot in _applianceSlotManager){
                    if(slot.currentItem != null) slot.EmptySlot();
                }
                Finished();
                numCuts = 0;
            }else{
                print("numCuts: " + numCuts);
                ++numCuts;
            }
        }
        
    }
}
