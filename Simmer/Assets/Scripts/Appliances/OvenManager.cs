using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Simmer.Items;
using Simmer.FoodData;

public class OvenManager : Simmer.Appliance.GenericAppliance
{
    const float _timeToBurn = 4.0f;
    protected override void Finished()
    {
        //@@TheUnaverageJoe@@MPerez132  4/26/2022
        //set finished variable to allow burning to start
        //-------------------------------------------------------
        //base.Finished();
        
        _soundManager.PlaySound(2, false);
        
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

        //mutliply by 50 because of 50 fixedUpdate cycles per second
        _progressBar.setMaxAmount(_timeToBurn*50);
        _progressBar.changeColor(false);
        _progressBar.incrementFill();

        _applianceSlotManager[0].currentItem.foodItem.timeProcessed += 0.02f;
        //consider doing sprite color adjustment here
        if(_applianceSlotManager[0].currentItem.foodItem.timeProcessed >= _timeToBurn){
            foreach(ItemSlotManager slot in _applianceSlotManager){
                if(slot.currentItem != null) slot.EmptySlot();
            }

            FoodItem burnt = new FoodItem(_burntFood, null);
            _applianceSlotManager[0].SpawnFoodItem(burnt);
            //Debug.Log("Spawned burnt item: " + burnt.itemName);
        }
    }

    public override void ToggleOn(){
        if(!_running){
            base.ToggleOn();
            if(_pendingTargetRecipe==null) return;
            _running = true;
            _UIManager._toggleButton.GetComponentInChildren<Text>().text = "Stop";
        }else{
            if(_pendingTargetRecipe!=null) return;
            for(int i=0; i<_applianceSlotManager.Count; i++){
                _applianceSlotManager[i].locking(false);
            }
            _progressBar.reset();
            _progressBar.changeColor(true);
            _UIManager._toggleButton.GetComponentInChildren<Text>().text = "Start";
            _running = false;
            _finished = false;
        }
    }
    //-------------------------------------------------
}
