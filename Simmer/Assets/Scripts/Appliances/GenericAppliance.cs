using System.Collections;
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
    [SerializeField] protected PlayerInventory playerInventory;

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
        FoodItem resultItem = new FoodItem(_pendingTargetRecipe.resultIngredient);
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
        List<RecipeData>[] allRecipeLists = new List<RecipeData>[currentIngredientList.Count];
        
        for(int k=0; k<currentIngredientList.Count; k++){
            IngredientData item = currentIngredientList[k];
            //Check if item belongs to this appliance
            bool keyExists = item.applianceRecipeListDict.ContainsKey(this._applianceData);
            if(!keyExists){
                Debug.Log("No bueno item in appliance");
                OnValidate.Invoke(null); return;
            }
            List<RecipeData> possibleRecipesList = item.applianceRecipeListDict[this.applianceData];
            allRecipeLists[k] = possibleRecipesList;
        }

        List<RecipeData> firstList = allRecipeLists[0];
        List<RecipeData> potentialRecipes = new List<RecipeData>(firstList);

/*
        parts.Sort(delegate(Part x, Part y)
        {
            if (x.PartName == null && y.PartName == null) return 0;
            else if (x.PartName == null) return -1;
            else if (y.PartName == null) return 1;
            else return x.PartName.CompareTo(y.PartName);
        });
*/

        //foreach(RecipeData recipe in firstList){
        //    print(recipe.name + " : ");
        //}
        //print("Before");

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

        //foreach(RecipeData recipe in firstList){
        //    print(recipe.name + " : ");
        //}

        print("STARTING PotentialRecipes: " + potentialRecipes.Count);

        foreach(RecipeData recipe in firstList){
            foreach(List<RecipeData> list in allRecipeLists)
            {
                print("list contains " + recipe + ": " + list.Contains(recipe));
                if(!list.Contains(recipe))
                {
                    if(!potentialRecipes.Contains(recipe)) break;

                    potentialRecipes.Remove(recipe);
                    break;
                }
                foreach(RecipeData checkRecipe in list){
                    print("recipe ingriedient count: " + recipe.ingredientDataList.Count +
                    "checkRecipe ingriedient count: " + checkRecipe.ingredientDataList.Count);
                    if(recipe.ingredientDataList.Count!=checkRecipe.ingredientDataList.Count){
                        potentialRecipes.Remove(recipe);
                        break;
                    }
                }
            }
            print("PotentialRecipes" + potentialRecipes.Count);
            if(potentialRecipes.Count == 1){
                break;
            }
        }
        print("PotentialRecipes: " + potentialRecipes.Count);
        if(potentialRecipes.Count == 1){
            OnValidate.Invoke(potentialRecipes[0]);
        }
        else{
            OnValidate.Invoke(null);
        }
    }
    
    private void OnValidateCallback(RecipeData recipe){
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
