using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Appliance;
using Simmer.Items;
using Simmer.UI;

public abstract class GenericAppliance : MonoBehaviour
{
    [SerializeField] protected ApplianceData _applianceData;
    public ApplianceData applianceData
    {
        get { return _applianceData; }
        set { applianceData = _applianceData; }
    }

    protected bool _running = false;
    protected FoodItem _toCook;
    protected FoodItem _finishedProcessing;
    private Clock timer;
    public Clock timerPrefab;

    void Awake()
    {
        timer = Instantiate(timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        timer.SetUpTimer(this.transform);
        _finishedProcessing = false;
    }
    public void AddItem(FoodItem recipe) {
        print(this + " AddItem : " + recipe.ingredientData);
        //add code for player Script to interact with this object
        _toCook = recipe;
    }

    public abstract FoodItem TakeItem();

    public void ToggleOn(){
        if(!_running)
        {
            Debug.Log("Toggling on");
            _running = true;
            timer.ShowClock();
            StartCoroutine(timer.SetTimer(4f, Finished));
        }
    }

    public void Finished() {
        Debug.Log("Toggling off");
        _running = false;
        timer.HideClock();
    }
}
