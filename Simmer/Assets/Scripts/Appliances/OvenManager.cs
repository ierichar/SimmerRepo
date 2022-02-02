using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;

public class OvenManager : GenericAppliance
{
    //private float _timeRunning;
    void Awake()
    {
        _timer = Instantiate(_timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _timer.SetUpTimer(this.transform);

        _invSize = 1;
        _toCook = new FoodItem[_invSize];

        _idle = true;
        _running = false;
        _finished = false;

        _timeRunning = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //update time running
        if(_running){
            _timeRunning += Time.deltaTime;
            //Debug.Log("Time: " + _timeRunning);
            
        }if(_idle){
            _timeRunning = 0.0f;
        }
    }

    public override void ToggleInventory(bool UIOpen){
        if(UIOpen){
            //pop up interactable UI window here
        }else{
            //close pop up interactable UI window here
        }
    }

    public override void TryInteract(FoodItem item){

        if(item != null)
        {
            if (!item.ingredientData.applianceRecipeDict
            .ContainsKey(_applianceData)) return;
        }

        if (_toCook==null)
        {
            RecipeData recipeData = item.ingredientData
                .applianceRecipeDict[_applianceData];
            _resultIngredient = recipeData.resultIngredient;
            //place item to be cooked
            AddItem(item);
            
            ToggleOn(recipeData.baseActionTime);
        }else if(_finished){
            FoodItem newFoodItem = new FoodItem(_resultIngredient);
            playerInventory.AddFoodItem(newFoodItem);
            _toCook = null;
        }
        else{
            //do something with appliance while cooking
        }
    }

    public override void AddItem(FoodItem recipe){
        print(this + " AddItem : " + recipe.ingredientData);
        //add code for player Script to interact with this object

        //FIX THIS HARD CODE INDCIE TO BE VARIABLE ADDRESSING DESIRED INV SLOT TO FILL
        _toCook[0] = recipe;
    }

    public override FoodItem TakeItem(){
        //code to take a finished product from the oven.
        FoodItem curr = null;//curr should be a FoodItem to be returned
        if(_toCook!=null && _running){
            Debug.Log("Take Item: " + (FoodItem)curr);
            //return new food item from recipes;
        }
        return null;
    }
    
    public override void ToggleOn(float duration){
        if(!_running)
        {
            Debug.Log("Toggling on");
            _running = true;
            _finished = false;
            _timer.ShowClock();
            StartCoroutine(_timer.SetTimer(duration, Finished));
        }
    }

    protected override void Finished() {
        Debug.Log("Toggling off");
        _running = false;
        _finished = true;
        _timer.HideClock();
    }
    
}
