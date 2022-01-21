using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simmer.Items;

public class OvenManager : GenericAppliance
{
    private float _timeRunning;
    private FoodItem _toCook;
    // Start is called before the first frame update
    void Start()
    {
        _timeRunning = 0.0f;
        _toCook = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //update time running
    }
    public void AddItem(){
        //add code for player Script to interact with this object

    }

    public void ToggleOn(){

    }


}
