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

        public VN_EventData(UnityEvent eventTarget
            , string eventCode)
        {
            this.eventTarget = eventTarget;
            this.eventCode = eventCode;
        }
    }
}