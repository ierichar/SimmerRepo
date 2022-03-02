using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Simmer.Items;
using Simmer.Interactable;

public class CuttingBoardManager : GenericAppliance
{
    private bool cuttingStarted;
    private int numCuts;

    //private ProgressBar _progressBar;

    [SerializeField] private int numCutsMultiplier;

    public override void Construct(ItemFactory itemFactory, UISoundManager soundManager, GameEventManager gameEventManager){
        print("Calling derived class constructor from cutting board");
        base.Construct(itemFactory, soundManager, gameEventManager);

        //derived class variable init
        //_progressBar = FindObjectOfType<ProgressBar>(true);

        cuttingStarted = false;
        numCuts = 0;
    }

     public override void FixedUpdate(){
         return;
     }

    public void chopping(){
        tryChop();
        if(!cuttingStarted) return;

        if(numCuts >= _pendingTargetRecipe.baseActionTime * numCutsMultiplier){
            foreach(ItemSlotManager slot in _applianceSlotManager){
                if(slot.currentItem != null) slot.EmptySlot();
            }
            Finished();
            cuttingStarted = false;
            numCuts = 0;
        }else{
            print("numCuts: " + numCuts);
            
            _progressBar.incrementFill();
            ++numCuts;
        }
    }

    private void tryChop(){
        if(!cuttingStarted){
            Validation();
            if(_pendingTargetRecipe == null) 
                return;
            else{
                _progressBar.reset();
                _progressBar.setMaxAmount(_pendingTargetRecipe.baseActionTime * numCutsMultiplier);
                cuttingStarted = true;
            }
        }
    }
}
