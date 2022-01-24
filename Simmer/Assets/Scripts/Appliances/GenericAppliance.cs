using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Appliance;

public abstract class GenericAppliance : MonoBehaviour
{
    [SerializeField] protected ApplianceData _applianceData;
    protected bool _running = false;
    public ApplianceData applianceData
    {
        get { return _applianceData; }
        set { applianceData = _applianceData; }
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
