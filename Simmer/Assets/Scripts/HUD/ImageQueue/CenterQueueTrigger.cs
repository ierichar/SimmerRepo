using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI.ImageQueue
{
    public class CenterQueueTrigger : MonoBehaviour
    {
        public QueueTrigger npcKnowledgeTrigger { get; private set; }
        public QueueTrigger npcQuestTrigger { get; private set; }

        public void Construct(ImageQueueManager imageQueueManager)
        {
            QueueTrigger[] queueTriggerArray = GetComponents<QueueTrigger>();
            npcKnowledgeTrigger = queueTriggerArray[0];
            npcQuestTrigger = queueTriggerArray[1];

            npcKnowledgeTrigger.Construct(imageQueueManager);
            npcQuestTrigger.Construct(imageQueueManager);
        }
    }
}