using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;
using Simmer.Interactable;

public class PlateManager : GenericAppliance
{
    public override void ToggleOn()
    {
        if(_applianceSlotManager[0].currentItem == null){
            return;
        }
    
        IngredientData firstIgredientData = _applianceSlotManager[0].currentItem.foodItem.ingredientData;
        if(firstIgredientData == null) return;

        bool keyExists = firstIgredientData
            .applianceRecipeListDict.ContainsKey(this._applianceData);
        if(!keyExists) return;

        //if(!_running)
        //    _running = true;
        //else
        //    return;

        
        _pendingTargetRecipe = firstIgredientData
            .applianceRecipeListDict[this._applianceData][0];
        UpdateCurrentIngredientList();

        Debug.Log("Combine button clicked");

        if(_pendingTargetRecipe.ingredientDataList.Count < currentIngredientList.Count){
            Debug.Log("not enough ingredient for the only possible recipe");
            //recipe cant be made, not enough ingredient for the only possible recipe
        }
        
        //For loop over all food items in mixerSlots
        foreach(IngredientData currFoodItem in currentIngredientList){
            //for loop over all ingredients required for the recipe
            bool wasIngredientFound = false;
            foreach(IngredientData currRecipeIngredientData in _pendingTargetRecipe.ingredientDataList){
                if(currFoodItem == currRecipeIngredientData){
                    wasIngredientFound = true;
                    break;
                }
            }
            if(!wasIngredientFound){
                Debug.Log("Ingredient " + currFoodItem.name + " not in the recipe " + _pendingTargetRecipe.name);
                return;
            }
        }
        if(currentIngredientList.Count != _pendingTargetRecipe.ingredientDataList.Count){
            Debug.Log("Not all ingredient present");
            _running = false;
            return;
        }

        //if we get here the ingredients are valid for the recipe
        Debug.Log("THE RECIPE WAS VALID AND WE ARE CLEARING THE MIXER_SLOTS_LIST");
        foreach(ItemSlotManager slot in _applianceSlotManager){
            if(slot !=null) slot.EmptySlot();
        }

        float duration = _pendingTargetRecipe.baseActionTime;
        //if(_timeRunning >= _pendingTargetRecipe.baseActionTime){

        //FINISHED SHOULD BE SET BY THE TIMER CLASS
        _finished = true;
        _running = false;

        //WARNING
        //MIXER CURRENTLY DOES NOT WAIT TO PRODUCE OUTPUT
        //HOWEVER CLOCK IS SHOWING FOR CONCEPT
        _timer.ShowClock();
        StartCoroutine(_timer.SetTimer(duration, Finished));
        
        /*
        if(_finished){
            //_mixerSlotManager[0].SetItem(new ItemBehaviour( , _pendingTargetRecipe.resultIngredient));
            Debug.Log("Possible Recipe name: " + _pendingTargetRecipe.name);
            _applianceSlotManager[0].SpawnFoodItem(resultItem);
        }
        */
    }
}
