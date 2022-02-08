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
    protected List<FoodItem> _toCook = new List<FoodItem>();
    //protected FoodItem[]_toCook;
    protected float _timeRunning = 0.0f;

    //protected bool _idle;
    protected bool _running;
    protected bool _finished;
    
    protected Clock _timer;
    public Clock _timerPrefab;
    protected IngredientData _resultIngredient;

    //-----------------------------------------------------------
    //inherited methods
    public abstract void ToggleInventory();
    public abstract void TryInteract(FoodItem item);
    public abstract void AddItem(FoodItem recipe);
    public abstract FoodItem TakeItem();
    public abstract void ToggleOn(float duration);
    protected abstract void Finished();
}
