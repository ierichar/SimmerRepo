using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
using Simmer.FoodData;

public class OvenManager : GenericAppliance
{   
    void FixedUpdate()
    {
        //update time running
        if(_running){
            _timeRunning += Time.deltaTime;
            //Debug.Log("Time: " + _timeRunning);
        }if(!_running){
            _timeRunning = 0.0f;
        }
    }
    public override void ToggleOn(){
        SpawningSlotManager slot = _applianceSlotManager[0];
        if(slot.currentItem == null){
            return;
        }
        IngredientData inputItem = slot.currentItem.foodItem.ingredientData;
        if(inputItem == null) return;

        bool keyExists = inputItem
            .applianceRecipeListDict.ContainsKey(this._applianceData);
        if(!keyExists) return;

        if(!_running)
            _running = true;
        else
            return;
            
        //---------------------------------------------------------
        //NOTE TEST IF NONE OVEN RECIPES ALSO GO THROUGH CONVERSION
        //Ask Evan where to find applianceRecipeListDict in unity project
        _pendingTargetRecipe = inputItem.applianceRecipeListDict[this._applianceData][0];
        if(slot !=null) slot.EmptySlot();
        float duration = _pendingTargetRecipe.baseActionTime;
        
        //StartCoroutine(waitForActionTime(_pendingTargetRecipe, slot));

        _timer.ShowClock();
        StartCoroutine(_timer.SetTimer(duration, Finished));
        
    }
}
