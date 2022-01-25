using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;

public abstract class GenericAppliance : MonoBehaviour
{

    [SerializeField] PlayerInventory playerInventory;

    [SerializeField] protected ApplianceData _applianceData;
    public ApplianceData applianceData
    {
        get { return _applianceData; }
        set { applianceData = _applianceData; }
    }

    protected bool _running = false;
    protected FoodItem _toCook;
    protected bool _finishedProcessing;
    private Clock timer;
    public Clock timerPrefab;

    private IngredientData resultIngredient;

    void Awake()
    {
        timer = Instantiate(timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        timer.SetUpTimer(this.transform);
        _finishedProcessing = false;
    }

    public void TryInteract(FoodItem item){

        if(item != null)
        {
            if (!item.ingredientData.applianceRecipeDict
            .ContainsKey(_applianceData)) return;
        }

        if (_toCook==null)
        {
            RecipeData recipeData = item.ingredientData
                .applianceRecipeDict[_applianceData];
            resultIngredient = recipeData.resultIngredient;
            //place item to be cooked
            AddItem(item);
            
            ToggleOn(recipeData.baseActionTime);
        }else if(_finishedProcessing){
            FoodItem newFoodItem = new FoodItem(resultIngredient);
            playerInventory.AddFoodItem(newFoodItem);
            _toCook = null;
        }
        else{
            //do something with appliance while cooking
        }
    }
    private void AddItem(FoodItem recipe) {
        print(this + " AddItem : " + recipe.ingredientData);
        //add code for player Script to interact with this object
        _toCook = recipe;
    }
    private FoodItem TakeItem(){
        //code to take a finished product from the oven.
        FoodItem curr = null;//curr should be a FoodItem to be returned
        if(_toCook!=null && _running){
            Debug.Log("Take Item: " + (FoodItem)curr);
            //return new food item from recipes;
        }
        return null;
    }

    private void ToggleOn(float duration){
        if(!_running)
        {
            Debug.Log("Toggling on");
            _running = true;
            _finishedProcessing = false;
            timer.ShowClock();
            StartCoroutine(timer.SetTimer(duration, Finished));
        }
    }

    private void Finished() {
        Debug.Log("Toggling off");
        _running = false;
        _finishedProcessing = true;
        timer.HideClock();
    }
}
