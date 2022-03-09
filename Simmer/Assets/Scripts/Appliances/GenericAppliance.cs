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
    protected ProgressBar _progressBar;

    protected InteractableBehaviour _interactable;

    protected static bool UI_OPEN = false;
    public ApplianceData applianceData
    {
        get { return _applianceData; }
        set { applianceData = _applianceData; }
    }

    //invSize should be defined by each individual applaince
    protected bool invOpen = false;
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
    private static GameObject _blackout;
    protected UISoundManager _soundManager;

    public UnityEvent<RecipeData> OnValidate = new UnityEvent<RecipeData>();

    //-----------------------------------------------------------
    //inherited methods
    public virtual void Construct(ItemFactory itemFactory, UISoundManager soundManager){
        _soundManager = soundManager;

        _interactable = GetComponent<InteractableBehaviour>();
        SpriteRendererManager highlightTarget
            = GetComponentInChildren<SpriteRendererManager>();
        highlightTarget.Construct();
        _interactable.Construct(ToggleInventory, highlightTarget, true);

        OnValidate.AddListener(OnValidateCallback);

        _timer = Instantiate(_timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _timer.SetUpTimer(this.transform);

        _UIManager.Construct(itemFactory);
        _UIGameObject = _UIManager.gameObject;

        _progressBar = _UIManager.GetComponentInChildren<ProgressBar>();
        _progressBar.Construct(10);

        if(_blackout==null)
            _blackout = GameObject.Find("Blackout");

        _UIGameObject.SetActive(false);
        _blackout.SetActive(false);

        _running = false;
        _finished = false;
        _timeRunning = 0.0f;

        _applianceSlotManager = _UIManager.slots.slots;

        if(GlobalPlayerData.AppInvSaveStruct.ContainsKey(applianceData)){
            List<FoodItem> temp = GlobalPlayerData.AppInvSaveStruct[applianceData];

            for(int k=0; k < temp.Count; ++k){
                SpawningSlotManager slot = _applianceSlotManager[k];
                slot.SpawnFoodItem(temp[k]);
            }
        }
    }

    public virtual void FixedUpdate(){
        if(_pendingTargetRecipe==null) return;

        _progressBar.setMaxAmount(_pendingTargetRecipe.baseActionTime*50);
        _progressBar.incrementFill();
    }
    public virtual void ToggleInventory(){
        if(!invOpen && !UI_OPEN){
            _UIGameObject.SetActive(true);
            _blackout.SetActive(true);
            invOpen = true;
            UI_OPEN = true;
        }else if(invOpen && UI_OPEN){
            _UIGameObject.SetActive(false);
            _blackout.SetActive(false);
            invOpen = false;
            UI_OPEN = false;
        }
    }
    protected virtual void Finished(){
        Debug.Log("finished waiting for action time");
        FoodItem resultItem = new FoodItem(_pendingTargetRecipe.resultIngredient, null);

        foreach(ItemSlotManager slot in _applianceSlotManager){
            if(slot.currentItem != null) slot.EmptySlot();
        }
        for(int i=0; i<_applianceSlotManager.Count; i++){
            _applianceSlotManager[i].locking(false);
        }

        _applianceSlotManager[0].SpawnFoodItem(resultItem);
        _pendingTargetRecipe = null;
        _timer.HideClock();
        _progressBar.reset();
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
            //Debug.Log("No bueno item in appliance");
            OnValidate.Invoke(null); return;
        }
        List<RecipeData> firstList = currentIngredientList[0]
            .applianceRecipeListDict[this._applianceData];

        foreach(RecipeData recipe in firstList){
            int recipeCount = recipe.ingredientDataList.Count;
            bool[] RecipeCheckArray = new bool[recipe.ingredientDataList.Count];

            if(currentIngredientList.Count != recipe.ingredientDataList.Count){
                //print("NOT THE CORRECT NUM ITEMS FOR: " + recipe.name);
                continue;
            }

            for(int k=0; k<recipeCount; ++k){
                IngredientData item = currentIngredientList[k];
                RecipeCheckArray[k] = recipe.ingredientDataList.Contains(item);
            }

            bool allTrue = Array.TrueForAll(RecipeCheckArray, (bool x)=>{
                return x;
            });

            if(allTrue){
                OnValidate.Invoke(recipe);
                return;
            }else{
                continue;
            }
        }
        //print("NO RECIPES FOUND");
        OnValidate.Invoke(null);
    }
    
    private void OnValidateCallback(RecipeData recipe){
        //print("INVOKED RECIPE: " + recipe);
        _pendingTargetRecipe = recipe;
        if(_pendingTargetRecipe != null){
            OnValidateCallbackPositive();
        }
        if(_pendingTargetRecipe == null){
            OnValidateCallbackNegative();
        }
    }
    protected virtual void OnValidateCallbackPositive(){
        for(int i=0; i<_applianceSlotManager.Count; i++){
            _applianceSlotManager[i].locking(true);
        }
    }
    protected virtual void OnValidateCallbackNegative(){
        _soundManager.PlaySound(7, false);
        _UIManager.GetNegFeedbackObjRef().SetActive(true);
        StartCoroutine(disableFeedbackObjAfter(2.0f));
    }

    public virtual void ToggleOn()
    {
        _soundManager.PlaySound(1, false);
        Validation();
        if(_pendingTargetRecipe == null){
            return;
        }
        float duration = _pendingTargetRecipe.baseActionTime;

        _timer.ShowClock();
        StartCoroutine(_timer.SetTimer(duration, Finished));
    }

    public virtual List<FoodItem> GetInventoryItems(){
        List<FoodItem> result = new List<FoodItem>();
        
        foreach(SpawningSlotManager inv in _applianceSlotManager){
            if(inv != null && inv.currentItem !=null)
                result.Add(inv.currentItem.foodItem);
        }
        return result;
    }

    public void SaveInventory(){
        if(GlobalPlayerData.AppInvSaveStruct.ContainsKey(applianceData))
            GlobalPlayerData.AppInvSaveStruct.Remove(applianceData);
        GlobalPlayerData.AppInvSaveStruct.Add(applianceData, GetInventoryItems());
    }

    protected IEnumerator disableFeedbackObjAfter(float time){
        yield return new WaitForSeconds(time);
        _UIManager.GetNegFeedbackObjRef().SetActive(false);
    }
    
}