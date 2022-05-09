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
        Color32 dayColor = new Color32(255, 255, 255, 255);
        private float minuteToRealTime = 0.07f;
        private float timer;
        

        private void OnEnable() 
        {
            TimeManager.OnMinuteChanged += changeLightingCallback;
        }

        private void OnDisable() 
        {
            TimeManager.OnMinuteChanged -= changeLightingCallback;
        }
        
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
            timer = minuteToRealTime;
            if(sceneLight==null){
                return;
            }else{
                /*
                if(AM){
                    if(Hour == 6){
                        if(Minute > 30){

                        }

                    }else if(Hour > 6){
                        if(Minute > 30){
                            
                        }
                    }
                }
                */
                if(((Hour==6 && Minute>=30 && AM) || ((Hour>6 && Hour!=12) && AM)) || ((Hour<7 || Hour==12) && AM==false))
                {
                    sceneLight.color = dayColor;
                }else{
                    Debug.Log(Hour + ":"+ Minute + "AM?: " + AM);
                    sceneLight.color = nightColor;
                }

                /*
                FEAST YOUR EYES ON THIS BROKEN LOGIC
                if((((Hour==6 && Minute>=30 && AM) || (Hour>6 && AM))&& (Hour<=11 && Minute<=59 && AM)) || 
                (Hour==12 && Minute>=0 && AM==false) || 
                ((Hour>=1 && Minute>=0 && AM==false) && (Hour<=6 && Minute<=59 && AM==false)))
                {
                    sceneLight.color = dayColor;
                }else{
                    Debug.Log(Hour + ":"+ Minute + "AM?: " + AM);
                    sceneLight.color = nightColor;
                }
                */
            }
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

        //@@MPerez132 @@TheUnaverageJoe 5/6/2022
        //-------------------------------------------
        private IEnumerator startLightingTransition(bool night, float timeToLerp){
            //float scaleLerp = 1/timeToLerp;
            float currTimer = 0;

            while(currTimer<timeToLerp){
                currTimer += 0.1f;

                if(night)
                    sceneLight.color = Color32.Lerp(dayColor, nightColor, Mathf.Clamp(currTimer/timeToLerp, 0, 1));
                else
                    sceneLight.color = Color32.Lerp(nightColor, dayColor, Mathf.Clamp(currTimer/timeToLerp, 0, 1));
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void changeLightingCallback(){
            if(sceneLight==null) return;

            if(Hour==7 && Minute==0 && AM == false){
                StartCoroutine(startLightingTransition(true, 10.0f));
            }else if(Hour==6 && Minute==30 && AM==true){
                StartCoroutine(startLightingTransition(false, 10.0f));
            }
        }
        
        //-------------------------------------------
    }
}
