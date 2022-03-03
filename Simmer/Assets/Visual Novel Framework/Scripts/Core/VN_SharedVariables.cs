using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Simmer.VN
{
    // Uses System.Reflection during runtime
    // Perhaps dangerous, needs further testing?
    public class VN_SharedVariables : MonoBehaviour
    {
        [Header("Ink/Unity shared variables")]
        public int isReturning = 0;
        public int isCorrectGift = 0;

        private List<VN_EventData> _eventDataList
            = new List<VN_EventData>();

        private Dictionary<string, UnityEvent>
            _eventDictionary = new Dictionary<string, UnityEvent>();

        private UnityEvent<string> eventDispactcher
            = new UnityEvent<string>();

        private FieldInfo[] FieldInfoArray;
        private VN_Manager manager;

        public void Construct(VN_Manager manager)
        {
            this.manager = manager;
            FieldInfoArray = GetType().GetFields();

            UnityEvent testEvent = new UnityEvent();
            testEvent.AddListener(() => Debug.Log("Hi"));

            VN_EventData testEventData = new VN_EventData(testEvent
                , "testEvent");

            _eventDataList.Add(testEventData);

            foreach(VN_EventData eventData in _eventDataList)
            {
                AddEventData(eventData);
            }
        }

        public void AddEventData(VN_EventData data)
        {
            _eventDictionary.Add(data.eventCode
                , data.eventTarget);

            eventDispactcher.AddListener(NewDispatch);
        }

        private void NewDispatch(string eventCode)
        {
            if(!_eventDictionary.ContainsKey(eventCode))
            {
                Debug.LogError(this + " Error: eventCode \""
                    + eventCode + "\" not found");
                return;
            }
            _eventDictionary[eventCode].Invoke();
        }

        public void SetVariable(string varName, string newValString)
        {
            Type T = this.GetType();
            FieldInfo toSet = T.GetField(varName);

            // Set field value to newValString
            toSet.SetValue(this,
                // Try to convert the type to the correct type
                Convert.ChangeType(newValString, toSet.FieldType));
        }

        public string GetVariableValue(string varName)
        {
            Type T = this.GetType();
            FieldInfo toGet = T.GetField(varName);

            return toGet.GetValue(this).ToString();
        }

        public UnityEvent InvokeEvent(string eventCode)
        {
            if (!_eventDictionary.ContainsKey(eventCode))
            {
                Debug.LogError(this + " Error: eventCode \""
                    + eventCode + "\" not found");
                return null;
            }

            eventDispactcher.Invoke(eventCode);

            return _eventDictionary[eventCode];
        }

        public UnityEvent GetEvent(string eventCode)
        {
            if (!_eventDictionary.ContainsKey(eventCode))
            {
                Debug.LogError(this + " Error: eventCode \""
                    + eventCode + "\" not found");
                return null;
            }

            return _eventDictionary[eventCode];
        }
    }
}