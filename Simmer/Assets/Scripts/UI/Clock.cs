using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.UI 
{
    public class Clock : MonoBehaviour 
    {
        //this is a temp variable, dont need total time after
        public float totalTime;
        public Image fillBar;
        public Camera cam;

        void Start() 
        {
            StartCoroutine(setTimer(totalTime, timerFinished));
        }

        public IEnumerator setTimer(float time, Action<float> action) 
        {
            float normTime = 0f;
            while(normTime <= 1f) {
                fillBar.fillAmount = normTime;
                normTime += Time.deltaTime / time;
                yield return null;
            }
            action(time);
        }

        public void timerFinished(float time) 
        {
            Debug.Log(time + " seconds has passed!");
            hideClock();
            StartCoroutine(wait());
        }

        //only useful if we want to move the clock
        public void updatePosition(GameObject target) {
            transform.position = cam.WorldToScreenPoint(target.transform.position);
        }

        private void hideClock() 
        {
            Image[] clockImage = GetComponentsInChildren<Image>();
            foreach(Image img in clockImage) 
            {
                img.enabled = false;
            }
        }

        private void showClock() 
        {
            Image[] clockImage = GetComponentsInChildren<Image>();
            foreach(Image img in clockImage) 
            {
                img.enabled = true;
            }
        }

        //random functions that just re-enables the clock for testing
        private IEnumerator wait() 
        {
            yield return new WaitForSeconds(2f);
            showClock();
            StartCoroutine(setTimer(10f, timerFinished));
        }
    }
}