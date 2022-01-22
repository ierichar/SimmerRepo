using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simmer.Items;

public class OvenManager : GenericAppliance
{
    private float _timeRunning;
    private FoodItem _toCook;
    private bool _running;

    // Start is called before the first frame update
    void Start()
    {
        _timeRunning = 0.0f;
        _toCook = null;
        _running = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //update time running
        if(_running){
            _timeRunning += Time.deltaTime;
            //Debug.Log("Time: " + _timeRunning);
            
        }else{
            _timeRunning = 0.0f;
        }
    }
    public void AddItem(FoodItem recipe) {
        print(this + " AddItem : " + recipe.ingredientData);
        //add code for player Script to interact with this object
        _toCook = recipe;
    }

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


}
