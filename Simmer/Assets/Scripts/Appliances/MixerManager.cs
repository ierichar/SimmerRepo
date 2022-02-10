using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;
using Simmer.Interactable;

public class MixerManager : GenericAppliance
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

    /*
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

        if(_pendingTargetRecipe != null) return;



        UpdateCurrentIngredientList();
        
        //_pendingTargetRecipe = firstIgredientData
        //    .applianceRecipeListDict[this._applianceData][0];
        foreach(RecipeData recipe in firstIgredientData.applianceRecipeListDict[this._applianceData]){
            //For loop over all food items in mixerSlots
            _pendingTargetRecipe = recipe;
            foreach(IngredientData currFoodItem in currentIngredientList){
                //for loop over all ingredients required for the recipe
                bool wasIngredientFound = false;
                foreach(IngredientData currRecipeIngredientData in recipe.ingredientDataList){
                    if(currFoodItem == currRecipeIngredientData){
                        wasIngredientFound = true;
                        break;
                    }
                }
                if(!wasIngredientFound){
                    Debug.Log("Ingredient " + currFoodItem.name + " not in the recipe " + recipe.name);
                    _running = false;
                    //return;
                }
            }
            if(currentIngredientList.Count != recipe.ingredientDataList.Count){
                Debug.Log("Not all ingredient present for: " + recipe.name);
                _running = false;
                //return;
            }
        }
        

        //if we get here the ingredients are valid for the recipe
        Debug.Log("THE RECIPE WAS VALID AND WE ARE CLEARING THE MIXER_SLOTS_LIST");
        foreach(ItemSlotManager slot in _applianceSlotManager){
            if(slot.currentItem != null) slot.EmptySlot();
            Debug.Log("Did we make it here");
        }

        float duration = _pendingTargetRecipe.baseActionTime;
        //if(_timeRunning >= _pendingTargetRecipe.baseActionTime){

        //WARNING
        //MIXER CURRENTLY DOES NOT WAIT TO PRODUCE OUTPUT
        //HOWEVER CLOCK IS SHOWING FOR CONCEPT
        _timer.ShowClock();
        StartCoroutine(_timer.SetTimer(duration, Finished));
    }
    */
}
