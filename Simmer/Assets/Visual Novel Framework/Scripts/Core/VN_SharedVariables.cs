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
        private VN_Manager manager;

        [Header("Ink/Unity shared variables")]
        public int isReturning = 0;
        public int isCorrectGift = 0;
        public int isQuestComplete = 0;
        public string questItem = "null";
        public string questReward = "null";

        private Dictionary<string, UnityEvent>
            _eventDictionary = new Dictionary<string, UnityEvent>();

        private UnityEvent<string> eventDispactcher
            = new UnityEvent<string>();

        public void Construct(VN_Manager manager)
        {
            this.manager = manager;

            eventDispactcher.AddListener(EventDispactcherCallback);
        }

        public void AddEventData(VN_EventData data)
        {
            if (_eventDictionary.ContainsKey(data.eventCode))
            {
                Debug.LogError(this + " Error: Cannot AddEventData " +
                    "on eventCode that already exists in _eventDictionary");
                return;
            }
            _eventDictionary.Add(data.eventCode
                , data.eventTarget);
        }

        public void RemoveEventData(VN_EventData data)
        {
            if(!_eventDictionary.ContainsKey(data.eventCode))
            {
                Debug.LogError(this + " Error: Cannot RemoveEventData " +
                    "on eventCode that doesn't exist in _eventDictionary");
                return;
            }
            _eventDictionary.Remove(data.eventCode);
        }

        private void EventDispactcherCallback(string eventCode)
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