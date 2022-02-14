using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;
using Simmer.FoodData;
using Simmer.Inventory;
using Simmer.Interactable;

public abstract class GenericAppliance : MonoBehaviour
{
    [SerializeField] protected ApplianceData _applianceData;

    [SerializeField]  protected ApplianceUIManager _UIManager;

    protected InteractableBehaviour interactable;

    protected static bool UI_OPEN = false;
    public ApplianceData applianceData
    {
        get { return _applianceData; }
        set { applianceData = _applianceData; }
    }

    //invSize should be defined by each individual applaince
    protected int _invSize;
    protected bool invOpen = false;
    protected int _currentNumItems = 0;
    protected float _cookingTimeMultiplier = 1.0f;
    protected List<SpawningSlotManager> _applianceSlotManager
        = new List<SpawningSlotManager>();
    protected List<IngredientData> currentIngredientList
        = new List<IngredientData>();
    protected float _timeRunning = 0.0f;

    //protected bool _idle;
    protected bool _running;
    protected bool _finished;
    
    protected Clock _timer;
    public Clock _timerPrefab;
    protected IngredientData _resultIngredient;
    protected RecipeData _pendingTargetRecipe;
    protected ItemFactory _itemFactory;
    protected GameObject _UIGameObject;

    public UnityEvent<RecipeData> OnValidate = new UnityEvent<RecipeData>();

    //-----------------------------------------------------------
    //inherited methods
    public virtual void Construct(ItemFactory itemFactory){

        interactable = GetComponent<InteractableBehaviour>();
        SpriteRenderer highlightTarget = GetComponentInChildren<SpriteRenderer>();
        interactable.Construct(ToggleInventory, highlightTarget);

        OnValidate.AddListener(OnValidateCallback);

        _timer = Instantiate(_timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _timer.SetUpTimer(this.transform);

        _UIManager.Construct(itemFactory);
        _UIGameObject = _UIManager.gameObject;

        _UIGameObject.SetActive(false);

        _running = false;
        _finished = false;
        _timeRunning = 0.0f;

        _applianceSlotManager = _UIManager.slots.slots;
    }
    public virtual void ToggleInventory(){
        if(!invOpen && !UI_OPEN){
            _UIGameObject.SetActive(true);
            invOpen = true;
            UI_OPEN = true;
        }else if(invOpen && UI_OPEN){
            _UIGameObject.SetActive(false);
            invOpen = false;
            UI_OPEN = false;
        }
    }
    protected virtual void Finished(){
        Debug.Log("finished waiting for action time");
        FoodItem resultItem = new FoodItem(_pendingTargetRecipe.resultIngredient
            , null);
        _applianceSlotManager[0].SpawnFoodItem(resultItem);
        _finished = true;
        //_running may need to stay true for burning of item
        _running = false;
        _timer.HideClock();
    }

    protected virtual void UpdateCurrentIngredientList(){
        currentIngredientList.Clear();
        foreach(ItemSlotManager peekItem in _applianceSlotManager){
            if(peekItem.currentItem != null){
                currentIngredientList.Add(peekItem.currentItem.foodItem.ingredientData);
            }
        }
    }

    protected void Validation()
    {
        UpdateCurrentIngredientList();

        if(currentIngredientList.Count == 0)
        {
            OnValidate.Invoke(null); return;
        }
        bool keyExists = currentIngredientList[0].applianceRecipeListDict
            .ContainsKey(this._applianceData);
        if(!keyExists){
            Debug.Log("No bueno item in appliance");
            OnValidate.Invoke(null); return;
        }
        List<RecipeData> firstList = currentIngredientList[0]
            .applianceRecipeListDict[this._applianceData];

/*
        firstList.Sort(delegate(RecipeData x, RecipeData y){
            int xCount = x.ingredientDataList.Count;
            int yCount = y.ingredientDataList.Count;
            if(xCount == yCount){
                return 0;
            }
            if(xCount>yCount){
                return -1;
            }else{
                return 1;
            }
        });
*/
        foreach(RecipeData recipe in firstList){
            int recipeCount = recipe.ingredientDataList.Count;
            bool[] RecipeCheckArray = new bool[recipe.ingredientDataList.Count];

            if(currentIngredientList.Count != recipe.ingredientDataList.Count){
                print("NOT THE CORRECT NUM ITEMS FOR: " + recipe.name);
                continue;
            }

            for(int k=0; k<recipeCount; ++k){
                IngredientData item = currentIngredientList[k];
                RecipeCheckArray[k] = recipe.ingredientDataList.Contains(item);
                //print(RecipeCheckArray[k]);
            }
            //print(RecipeCheckArray.ToString() + " ______________");

            bool allTrue = Array.TrueForAll(RecipeCheckArray, (bool x)=>{
                return x;
            });

            //print("AllTrue is: " + allTrue);

            if(allTrue){
                OnValidate.Invoke(recipe);
                return;
            }else{
                continue;
            }
        }
        print("NO RECIPES FOUND");
        OnValidate.Invoke(null);
    }
    
    private void OnValidateCallback(RecipeData recipe){
        print("INVOKED RECIPE: " + recipe);
        _pendingTargetRecipe = recipe;
    }

    public virtual void ToggleOn()
    {
        Validation();
        if(_pendingTargetRecipe == null) return;
        
        //if we get here the ingredients are valid for the recipe
        Debug.Log("THE RECIPE WAS VALID AND WE ARE CLEARING THE MIXER_SLOTS_LIST");
        foreach(ItemSlotManager slot in _applianceSlotManager){
            if(slot.currentItem != null) slot.EmptySlot();
        }

        float duration = _pendingTargetRecipe.baseActionTime;

        _timer.ShowClock();
        StartCoroutine(_timer.SetTimer(duration, Finished));
    }
}