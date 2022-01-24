using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.UI 
{
    public class Clock : MonoBehaviour 
    {
        public Image fillBar;

        void Start() 
        {
            // This is bad
            this.transform.SetParent(GameObject.Find("PlayCanvas").transform, false);
        }

        public IEnumerator SetTimer(float time, Action action) 
        {
            float normTime = 0f;
            while(normTime <= 1f) {
                fillBar.fillAmount = normTime;
                normTime += Time.deltaTime / time;
                yield return null;
            }
            action();
        }

        // public void timerFinished(float time) 
        // {
        //     Debug.Log(time + " seconds has passed!");
        //     hideClock();
        //     StartCoroutine(wait());
        // }

        public void SetUpTimer(Transform target) {
            HideClock();
            UpdatePosition(target);
        }

        //only useful if we want to move the clock
        public void UpdatePosition(Transform target) {
            //Vector2 canvasPos = Camera.main.WorldToScreenPoint(target.position);
        }

        public void HideClock() 
        {
            Image[] clockImage = GetComponentsInChildren<Image>();
            foreach(Image img in clockImage) 
            {
                img.enabled = false;
            }
        }

        public void ShowClock() 
        {
            Image[] clockImage = GetComponentsInChildren<Image>();
            foreach(Image img in clockImage) 
            {
                img.enabled = true;
            }
        }

        // //random functions that just re-enables the clock for testing
        // private IEnumerator wait() 
        // {
        //     yield return new WaitForSeconds(2f);
        //     showClock();
        //     StartCoroutine(setTimer(10f, timerFinished));
        // }
    }
}