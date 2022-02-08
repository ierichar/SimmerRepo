using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;
using Simmer.Interactable;

public class OvenManager : GenericAppliance
{
    private List<SpawningSlotManager> _inventorySlotManagerList
            = new List<SpawningSlotManager>();
    
    private GameObject myInv;
    private GameObject ovenSlots;
    private ItemFactory _itemFactory;
    public void Construct()
    {
        interactable = GetComponent<InteractableBehaviour>();
        SpriteRenderer highlightTarget = GetComponentInChildren<SpriteRenderer>();
        interactable.Construct(ToggleInventory, highlightTarget);

        _timer = Instantiate(_timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _timer.SetUpTimer(this.transform);

        _invSize = 1;
        myInv = GameObject.Find("OvenInventory");
        ovenSlots = GameObject.Find("OvenSlots");
        _itemFactory = FindObjectOfType<ItemFactory>();
        myInv.SetActive(false);
        

        //_idle = true;
        _running = false;
        _finished = false;

        _timeRunning = 0.0f;

        // Will get them in order of Scene Hierarchy from top to bottom
        SpawningSlotManager[] itemSlotArray
            = ovenSlots.GetComponentsInChildren<SpawningSlotManager>();

        for (int i = 0; i < _invSize; ++i)
        {
            SpawningSlotManager thisSlot = itemSlotArray[i];

            _inventorySlotManagerList.Add(thisSlot);
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
    
    public override void ToggleOn(float duration){
        SpawningSlotManager slot = _inventorySlotManagerList[0];
        if(slot.currentItem == null){
            return;
        }
        if(!_running)
            _running = true;
        else
            return;

        IngredientData inputItem = slot.currentItem.foodItem.ingredientData;
        //---------------------------------------------------------
        //NOTE TEST IF NONE OVEN RECIPES ALSO GO THROUGH CONVERSION
        //Ask Evan where to find applianceRecipeListDict in unity project
        RecipeData possibleRecipe = inputItem.applianceRecipeListDict[this._applianceData][0];
        if(slot !=null) slot.EmptySlot();
        duration = possibleRecipe.baseActionTime;
        
        StartCoroutine(waitForActionTime(possibleRecipe, slot));

        _timer.ShowClock();
        StartCoroutine(_timer.SetTimer(duration, Finished));
        
    }

    protected override void Finished() {
        _timer.HideClock();
    }

    IEnumerator waitForActionTime(RecipeData recipe, SpawningSlotManager slot){
        while(_timeRunning <= recipe.baseActionTime){
            Debug.Log("timeRunning is: " + _timeRunning);
            yield return null;
        }
        Debug.Log("finished waiting for action time");
        FoodItem resultItem = new FoodItem(recipe.resultIngredient);
        slot.SpawnFoodItem(resultItem);
        _finished = true;
        //_running may need to stay true for burning of item
        _running = false;
    }
    
}
