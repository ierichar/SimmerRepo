using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Simmer.VN
{
    public class VN_EventData
    {
        public UnityEvent eventTarget { get; private set; }

        public string eventCode { get; private set; }

        private UnityEvent<string> _dispatcherEvent;

        public VN_EventData(UnityEvent eventTarget
            , string eventCode)
        {
            this.eventTarget = eventTarget;
            this.eventCode = eventCode;
            //_dispatcherEvent = dispatcherRef;

            //_dispatcherEvent.AddListener(DispatchAction);
        }

        //private void DispatchAction(string eventCode)
        //{
        //    eventTarget.Invoke();
        //}

        //// Destructor
        //~VN_EventData()
        //{
        //    _dispatcherEvent.RemoveListener(DispatchAction);
        //}
    }
}