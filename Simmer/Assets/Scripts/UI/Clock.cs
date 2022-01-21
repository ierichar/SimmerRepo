using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.UI 
{
    public class Clock : MonoBehaviour 
    {
        public float totalTime;

        public Image fillBar;

        void Start() {
            StartCoroutine(SetTimer(totalTime, timerFinished));
        }

        //These are so that you can use in another function, that way you can
        public IEnumerator SetTimer(float time, Action<float> action) {
            float normTime = 0f;
            while(normTime <= 1f) {
                fillBar.fillAmount = normTime;
                normTime += Time.deltaTime / time;
                yield return null;
            }
            action(time);
        }

        public void timerFinished(float time) {
            Debug.Log(time + " seconds has passed!");
        }
    }
}