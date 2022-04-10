using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TimeManager : MonoBehaviour
{
    
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }


    private float minuteToRealTime = 2f;
    private float timer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Minute = 0;
        Hour = 06;
        timer = minuteToRealTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            Minute++;
            OnMinuteChanged?.Invoke();
            if(Minute >= 60) {
                Hour++;
                OnHourChanged?.Invoke();
                Minute = 0;
                if(Hour > 12) {
                    Hour = 1;
                    OnHourChanged?.Invoke();
                }
            }
            timer = minuteToRealTime;
        }
    }
}
