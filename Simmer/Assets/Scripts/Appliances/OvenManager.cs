using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simmer.Items;
using Simmer.UI;

public class OvenManager : GenericAppliance
{
    public Clock timerPrefab;
    private float _timeRunning;
    private FoodItem _toCook;
    private bool _running;
    private Clock timer;

    // Start is called before the first frame update
    void Start()
    {
        _timeRunning = 0.0f;
        _toCook = null;
        _running = false;
        timer = Instantiate(timerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        timer.SetUpTimer(this.transform);
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
            timer.ShowClock();
            StartCoroutine(timer.SetTimer(2f, Finished));
        }
    }

    public void Finished() {
        Debug.Log("Toggling off");
        _running = false;
        timer.HideClock();
    }
}
