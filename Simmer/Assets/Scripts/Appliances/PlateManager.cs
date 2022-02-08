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
    private List<SpawningSlotManager> _plateSlotManager
            = new List<SpawningSlotManager>();
    
    private GameObject myInv;
    private GameObject plateSlots;
    private ItemFactory _itemFactory;

    public void Construct()
    {
        interactable = GetComponent<InteractableBehaviour>();
        SpriteRenderer highlightTarget = GetComponentInChildren<SpriteRenderer>();
        interactable.Construct(ToggleInventory, highlightTarget);

        _timer = Instantiate(_timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _timer.SetUpTimer(this.transform);

        _invSize = 2;
        myInv = GameObject.Find("PlateInventory");
        plateSlots = GameObject.Find("PlateSlots");
        _itemFactory = FindObjectOfType<ItemFactory>();
        myInv.SetActive(false);
        

        //_idle = true;
        _running = false;
        _finished = false;

        _timeRunning = 0.0f;

        // Will get them in order of Scene Hierarchy from top to bottom
        SpawningSlotManager[] itemSlotArray
            = plateSlots.GetComponentsInChildren<SpawningSlotManager>();

        for (int i = 0; i < _invSize; ++i)
        {
            SpawningSlotManager thisSlot = itemSlotArray[i];

            _plateSlotManager.Add(thisSlot);
            thisSlot.Construct(_itemFactory, i);
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
        IngredientData firstIgredientData = _plateSlotManager[0].currentItem.foodItem.ingredientData;
        IngredientData secondItem = _plateSlotManager[1].currentItem.foodItem.ingredientData;

        if(secondItem == null || firstIgredientData==null) return;

        // TODO May not be 0 index, change check to all possible
        List<RecipeData> possibleRecipes = firstIgredientData
            .applianceRecipeListDict[this._applianceData];

        RecipeData selectedRecipe = null;

        foreach(RecipeData recipe in possibleRecipes){
            //check if second ingredient is part of one of
            //the recipes belonging to the first  ingredient
            if(recipe.ingredientDataList.Contains(secondItem)){
                selectedRecipe = recipe;
            }
        }
        if(selectedRecipe == null){
            Debug.Log("Not a valid combination recipe");
            return;
        }
        foreach(ItemSlotManager slot in _plateSlotManager){
            slot.EmptySlot();
        }
        FoodItem resultItem = new FoodItem(selectedRecipe.resultIngredient);
        _plateSlotManager[0].SpawnFoodItem(resultItem);
    }

    protected override void Finished(){
        //_timer.HideClock();
    }

}
