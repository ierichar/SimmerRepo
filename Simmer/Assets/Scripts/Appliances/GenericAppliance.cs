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

    //-----------------------------------------------------------
    //inherited methods
    public virtual void Construct(ItemFactory itemFactory){

        interactable = GetComponent<InteractableBehaviour>();
        SpriteRenderer highlightTarget = GetComponentInChildren<SpriteRenderer>();
        interactable.Construct(ToggleInventory, highlightTarget);

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
    public abstract void ToggleOn();
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
}
