using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.UI.ImageQueue
{
    public class QueuePosition : MonoBehaviour
    {
        public RectTransform rectTransform { get; private set; }

        public QueueItem queueItem { get; private set; }

        public void Construct()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetQueueItem(QueueItem queueItem)
        {
            this.queueItem = queueItem;
        }
    }
}