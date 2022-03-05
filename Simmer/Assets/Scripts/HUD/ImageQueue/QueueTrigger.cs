using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.ImageQueue
{
    public class QueueTrigger : MonoBehaviour
    {
        private ImageQueueManager _imageQueueManager;

        public RectTransform rectTransform { get; private set; }

        public void Construct(ImageQueueManager imageQueueManager)
        {
            _imageQueueManager = imageQueueManager;

            rectTransform = GetComponent<RectTransform>();
        }

        public void SpawnQueueItem(IngredientData toSpawn)
        {
            _imageQueueManager.AddQueueItem(toSpawn, this);
        }
    }
}