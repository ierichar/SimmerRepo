using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;

public class MixerManager : GenericAppliance
{
    private List<ItemSlotManager> _mixerSlotManager
            = new List<ItemSlotManager>();
    
    private GameObject myInv;
    private GameObject mixerSlots;

    private List<IngredientData> currentIngredientList = new List<IngredientData>();

    public void Construct()
    {
        _timer = Instantiate(_timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _timer.SetUpTimer(this.transform);

        _invSize = 6;
        myInv = GameObject.Find("MixerInventory");
        mixerSlots = GameObject.Find("MixerSlots");
        myInv.SetActive(false);
        

        //_idle = true;
        _running = false;
        _finished = false;

        _timeRunning = 0.0f;

        // Will get them in order of Scene Hierarchy from top to bottom
        ItemSlotManager[] itemSlotArray
            = mixerSlots.GetComponentsInChildren<ItemSlotManager>();

        for (int i = 0; i < _invSize; ++i)
        {
            ItemSlotManager thisSlot = itemSlotArray[i];

            _mixerSlotManager.Add(thisSlot);
            thisSlot.Construct(i);
        }
    }
    
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

    public override void ToggleInventory(){
        if(!invOpen && !UI_OPEN){
            myInv.SetActive(true);
            invOpen = true;
            UI_OPEN = true;
        }else if(invOpen && UI_OPEN){
            myInv.SetActive(false);
            invOpen = false;
            UI_OPEN = false;
        }
    }
    public override void TryInteract(FoodItem item){

    }
    public override void AddItem(FoodItem recipe){

    }
    public override FoodItem TakeItem(){
        return null;
    }
    public override void ToggleOn(float duration){
        if(!_running) _running = true;

        IngredientData firstIgredientData = _mixerSlotManager[0].currentItem.foodItem.ingredientData;

        RecipeData possibleRecipes = firstIgredientData.applianceRecipeDict[this._applianceData];


        //PICK UP RECIPE VALIDATION HERE

    }
    protected override void Finished(){

    }

    private void UpdateCurrentIngredientList(){
        currentIngredientList.Clear();
        foreach(ItemSlotManager peekItem in _mixerSlotManager){
            if(peekItem.currentItem != null){
                currentIngredientList.Add(peekItem.currentItem.foodItem.ingredientData);
            }
        }
    }
}
