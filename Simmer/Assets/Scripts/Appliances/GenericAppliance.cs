using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Appliance;
using Simmer.Items;

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
    public void ToggleOn(){
        if(!_running)
        {
            Debug.Log("Toggling on");
            _running = true;
        }else{
            Debug.Log("Toggling off");
            _running = false;
        }
    }
    public void AddItem(FoodItem recipe) {
        print(this + " AddItem : " + recipe.ingredientData);
        //add code for player Script to interact with this object
        _toCook = recipe;
    }
}
