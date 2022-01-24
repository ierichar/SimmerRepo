using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simmer.Items;

public class OvenManager : GenericAppliance
{
    private float _timeRunning;
    
    // Start is called before the first frame update
    void Start()
    {
        _timeRunning = 0.0f;
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
    
}
