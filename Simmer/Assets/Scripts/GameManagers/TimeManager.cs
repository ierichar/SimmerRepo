using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Simmer.CustomTime{
    public class TimeManager : MonoBehaviour
    {
        
        public static Action OnMinuteChanged;
        public static Action OnHourChanged;
        

        public static int Minute { get; private set; }
        public static int Hour { get; private set; }
        public static bool AM { get; private set;}


        private float minuteToRealTime = 0.07f;
        private float timer;
        
        
        // Start is called before the first frame update
        void Start()
        {
            Hour = GlobalPlayerData.currentTime[0];
            Minute = GlobalPlayerData.currentTime[1];
            if(GlobalPlayerData.currentTime[2]==0){
                AM = true;
            }else if(GlobalPlayerData.currentTime[2]==1){
                AM = false;
            }else{
                Debug.LogError("AM was not equal to 0 or 1");
            }
            //Minute = 0;
            //Hour = 6;
            timer = minuteToRealTime;
            //AM = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            timer -= Time.deltaTime;
            if(timer <= 0) {
                Minute++;
                if(Minute >= 60) {
                    Minute = 0;
                    Hour++;
                    OnHourChanged?.Invoke();
                    if(Hour == 12){
                        AM = !AM;
                    }
                    if(Hour > 12) {
                        Hour = 1;
                        OnHourChanged?.Invoke();
                    }
                }
                OnMinuteChanged?.Invoke();
                timer = minuteToRealTime;
            }
        }
    }
}
