using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;

public class StoveManager : GenericAppliance
{
    protected override void Finished()
    {
        //@@TheUnaverageJoe@@MPerez132  4/26/2022
        //set finished variable to allow burning to start
        //-------------------------------------------------------
        //base.Finished();
        
        _soundManager.PlaySound(4, false);
        
        Debug.Log("finished waiting for action time");
        FoodItem resultItem = new FoodItem(_pendingTargetRecipe.resultIngredient, null);

        foreach(ItemSlotManager slot in _applianceSlotManager){
            if(slot.currentItem != null) slot.EmptySlot();
        }
        //for(int i=0; i<_applianceSlotManager.Count; i++){
        //    _applianceSlotManager[i].locking(false);
        //}

        _applianceSlotManager[0].SpawnFoodItem(resultItem);
        _pendingTargetRecipe = null;
        _timer.HideClock();
        _progressBar.reset();

        _finished = true;
        //-------------------------------------------------------
    }

    //@@TheUnaverageJoe@@MPerez132 4/26/2022
    //Fixed update used to apply burning to food item
    //-------------------------------------------------
    new void FixedUpdate(){
        base.FixedUpdate();
        if(!_finished) return;
        _applianceSlotManager[0].currentItem.foodItem.timeProcessed += 0.02f;
        //consider doing sprite color adjustment here
        
        Debug.Log("Time Processed: " + _applianceSlotManager[0].currentItem.foodItem.timeProcessed);
    }

    public override void ToggleOn(){
        if(!_running){
            base.ToggleOn();
            _running = true;
        }else{
            for(int i=0; i<_applianceSlotManager.Count; i++){
                _applianceSlotManager[i].locking(false);
            }
            _running = false;
            _finished = false;
        }
    }
    //-------------------------------------------------
}
