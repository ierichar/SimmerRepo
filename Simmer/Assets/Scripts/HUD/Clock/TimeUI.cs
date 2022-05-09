using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Simmer.CustomTime;
public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void OnEnable() 
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        //TimeManager.OnHourChanged += UpdateTime;
        string halfDay;
        if(TimeManager.AM){
            halfDay = "AM";
        }else{
            halfDay = "PM";
        }
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00} {halfDay} \nDay {TimeManager.Day}";
    }

    private void OnDisable() 
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        //TimeManager.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        string halfDay;
        if(TimeManager.AM){
            halfDay = "AM";
        }else{
            halfDay = "PM";
        }
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00} {halfDay} \nDay {TimeManager.Day}";
    }
}
