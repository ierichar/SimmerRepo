using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Simmer.CustomTime{
    public class TimeManager : MonoBehaviour
    {
        
        [SerializeField] private Light sceneLight;
        public static Action OnMinuteChanged;
        public static Action OnHourChanged;
        public static int Minute { get; private set; }
        public static int Hour { get; private set; }
        public static bool AM { get; private set; }
        public static int Day { get; private set; }
        public static bool Paused { get; private set; }

        Color32 nightColor = new Color32(34, 93, 154, 255);
        Color32 dayColor = new Color32(0, 0, 0, 255);
        private float minuteToRealTime = 0.07f;
        private float timer;
        
        
        // Start is called before the first frame update
        void Start()
        {
            Hour = GlobalPlayerData.currentTime[0];
            Minute = GlobalPlayerData.currentTime[1];
            Day = GlobalPlayerData.currentTime[3];
            if(GlobalPlayerData.currentTime[2]==0){
                AM = true;
            }else if(GlobalPlayerData.currentTime[2]==1){
                AM = false;
            }else{
                Debug.LogError("AM was not equal to 0 or 1");
            }
            //@@MPerez132 @@TheUnaverageJoe 5/2/2022
            // added pause feature ----------------------------
            if(GlobalPlayerData.currentTime[4]==0){
                Paused = true;
            }else if(GlobalPlayerData.currentTime[4]==1){
                Paused = false;
            }else{
                Debug.LogError("AM was not equal to 0 or 1");
            }
            // -------------------------------------------------
            //Minute = 0;
            //Hour = 6;
            timer = minuteToRealTime;
            
            //AM = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(Paused) return;
            timer -= Time.deltaTime;
            if(timer <= 0) {
                Minute++;
                if(Minute >= 60) {
                    Minute = 0;
                    Hour++;
                    OnHourChanged?.Invoke();
                    if(Hour == 12){
                        AM = !AM;
                        if(AM){
                            Day++;
                        }
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

        //@@MPerez132 @@TheUnaverageJoe 5/2/2022
        //-------------------------------------------
        public void SetPause(bool shouldPause){
            if(shouldPause){
                Paused = true;
            }else{
                Paused = false;
            }
        }

        //optional parameters are those which have an "=" following the variable
        // Design choice, dont pass in day as it is implied we only ever would want to go to the next day
        public void SetTime(int hour=8, int minute=0, bool am=true, bool paused=false){
            //following if statements enable the ability to implicitly increment 
            //  the day under the assumtion we cant go back in time
            if(hour < Hour){
                Day++;
            }
            Hour = hour;
            Minute = minute;
            AM = am;
            //give users the choice to pause but really doesnt make sense in most cases
            Paused = paused;
        }
        //-------------------------------------------

        //@@MPerez132 @@TheUnaverageJoe 5/4/2022
        //-------------------------------------------
        private void startLightingTransition(bool night, float scaleValue){

            if(night){
                sceneLight.color = Color32.Lerp(dayColor, nightColor, scaleValue);
            }else{
                sceneLight.color = Color32.Lerp(nightColor, dayColor, scaleValue);
            }
            

            // byte RedChangePerSec = (nightColor.r - dayColor.r) / timeToLerp;
            // byte GreenChangePerSec = (nightColor.g - dayColor.g) / timeToLerp;
            // byte BlueChangePerSec = (nightColor.b - dayColor.b) / timeToLerp;
            // Color32 currColor;
            // Color32 targetColor;
            // if(night){
            //     currColor = dayColor;
            //     targetColor = nightColor;
            // }else{
            //     currColor = nightColor;
            //     targetColor = dayColor;
            // }
            // while(currColor.r != targetColor.r && currColor.g != targetColor.g && currColor.b != targetColor.b){
            //     currColor.r = currColor.r + ToByte(RedChangePerSec);
                
            //     yield return new WaitForSeconds(1);
            // }
            
        }
        
        //-------------------------------------------
    }
}
