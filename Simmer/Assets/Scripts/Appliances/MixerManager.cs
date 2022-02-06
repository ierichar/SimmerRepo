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
    private List<SpawningSlotManager> _mixerSlotManager
            = new List<SpawningSlotManager>();
    
    private GameObject myInv;
    private GameObject mixerSlots;
    private ItemFactory _itemFactory;

    private List<IngredientData> currentIngredientList = new List<IngredientData>();

    public void Construct()
    {
        _timer = Instantiate(_timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _timer.SetUpTimer(this.transform);

        _invSize = 6;
        myInv = GameObject.Find("MixerInventory");
        mixerSlots = GameObject.Find("MixerSlots");
        _itemFactory = FindObjectOfType<ItemFactory>();
        myInv.SetActive(false);
        

        //_idle = true;
        _running = false;
        _finished = false;

        _timeRunning = 0.0f;

        // Will get them in order of Scene Hierarchy from top to bottom
        SpawningSlotManager[] itemSlotArray
            = mixerSlots.GetComponentsInChildren<SpawningSlotManager>();

        for (int i = 0; i < _invSize; ++i)
        {
            SpawningSlotManager thisSlot = itemSlotArray[i];

            _mixerSlotManager.Add(thisSlot);
            thisSlot.Construct(_itemFactory, i);
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
        // TODO May not be 0 index, change check to all possible
        RecipeData possibleRecipe = firstIgredientData
            .applianceRecipeListDict[this._applianceData][0];
        //int ingredientCounter = 0;
        UpdateCurrentIngredientList();

        if(possibleRecipe.ingredientDataList.Count < currentIngredientList.Count){
            Debug.Log("not enough ingredient for the only possible recipe");
            //recipe cant be made, not enough ingredient for the only possible recipe
        }
        
        //For loop over all food items in mixerSlots
        foreach(IngredientData currFoodItem in currentIngredientList){
            //for loop over all ingredients required for the recipe
            bool wasIngredientFound = false;
            foreach(IngredientData currRecipeIngredientData in possibleRecipe.ingredientDataList){
                if(currFoodItem == currRecipeIngredientData){
                    wasIngredientFound = true;
                    //ingredientCounter++;
                    break;
                }
            }
            if(!wasIngredientFound){
                Debug.Log("Ingredient " + currFoodItem.name + " not in the recipe " + possibleRecipe.name);
                return;
            }
        }
        if(currentIngredientList.Count != possibleRecipe.ingredientDataList.Count){
            Debug.Log("Not all ingredient present");
            return;
        }

        //if we get here the ingredients are valid for the recipe
        Debug.Log("THE RECIPE WAS VALID AND WE ARE CLEARING THE MIXER_SLOTS_LIST");
        foreach(ItemSlotManager slot in _mixerSlotManager){
            if(slot !=null) slot.EmptySlot();
        }
        FoodItem resultItem = new FoodItem(possibleRecipe.resultIngredient);
        //if(_timeRunning >= possibleRecipe.baseActionTime){

        //FINISHED SHOULD BE SET BY THE TIMER CLASS
        _finished = true;
        _running = false;
        
        if(_finished){
            //_mixerSlotManager[0].SetItem(new ItemBehaviour( , possibleRecipe.resultIngredient));
            Debug.Log("Possible Recipe name: " + possibleRecipe.name);
            _mixerSlotManager[0].SpawnFoodItem(resultItem);
        }
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
