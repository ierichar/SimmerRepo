using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

using Simmer.FoodData;

namespace Simmer.UI.ImageQueue
{
    public class ImageQueueManager : MonoBehaviour
    {
        public RectTransform rectTransform { get; private set; }

        [SerializeField] private QueueItem _queueItemPrefab;
        [SerializeField] private float _queuePauseDuration;
        public Ease itemMoveEase;
        public float itemMoveDuration;
        public string itemTooltipHeader;

        private List<QueuePosition> _queuePositionList
            = new List<QueuePosition>();

        public UnityEvent OnStartQueue = new UnityEvent();
        public List<QueueItem> itemQueue;
        private Coroutine _currentQueueProcess = null;

        private bool _isProcessing = false;

        public void Construct()
        {
            rectTransform = GetComponent<RectTransform>();

            itemQueue = new List<QueueItem>();

            QueuePosition[] queuePositionArray =
                GetComponentsInChildren<QueuePosition>();

            foreach (var queuePosition in queuePositionArray)
            {
                _queuePositionList.Add(queuePosition);
                queuePosition.Construct();
            }
            // Gets item from left to right but need first position
            // to be closest to the destination on the right rather
            // than furthest
            _queuePositionList.Reverse();

            OnStartQueue.AddListener(OnStartQueueCallback);
        }

        private void OnStartQueueCallback()
        {
            print("OnStartQueueCallback");
            if (_currentQueueProcess != null) return;
            _currentQueueProcess = StartCoroutine(OnStartQueueSequence());
        }

        private IEnumerator OnStartQueueSequence()
        {
            _isProcessing = true;
            while (_isProcessing)
            {
                yield return StartCoroutine(ProcessQueueStep());
            }
            _currentQueueProcess = null;
        }


        public void AddQueueItem(IngredientData ingredient
            , QueueTrigger originQueueTrigger)
        {
            StartCoroutine(AddQueueItemSequeunce(ingredient
                , originQueueTrigger));
        }

        private IEnumerator AddQueueItemSequeunce(IngredientData ingredient
            , QueueTrigger originQueueTrigger)
        {
            QueueItem newQueueItem = Instantiate(_queueItemPrefab
                , originQueueTrigger.transform);
            newQueueItem.Construct(ingredient, originQueueTrigger, this);

            QueuePosition nextQueuePosition = GetNextQueuePosition();

            yield return StartCoroutine(newQueueItem.MoveToQueueSequence(
                nextQueuePosition));
        }

        private IEnumerator ProcessQueueStep()
        {
            yield return new WaitForSeconds(_queuePauseDuration);

            for (int i = 1; i < _queuePositionList.Count; ++i)
            {
                QueuePosition previousPosition = _queuePositionList[i];
                QueueItem queueItem = previousPosition.queueItem;
                if (queueItem == null) continue;

                QueuePosition newPosition = _queuePositionList[i - 1];

                StartCoroutine(queueItem.MoveItem(
                    newPosition.rectTransform));
                newPosition.SetQueueItem(queueItem);
                previousPosition.SetQueueItem(null);
            }

            yield return new WaitForSeconds(itemMoveDuration);

            QueueItem firstItem = _queuePositionList[0].queueItem;
            if (firstItem != null)
            {
                StartCoroutine(firstItem.FadeDestroy());
            }

            if (itemQueue.Count == 0) _isProcessing = false;
        }

        private QueuePosition GetNextQueuePosition()
        {
            // Skip first position since that's the one
            // on the destination
            for (int i = 1; i < _queuePositionList.Count; ++i)
            {
                QueuePosition queuePosition = _queuePositionList[i];

                if (queuePosition.queueItem == null)
                {
                    return queuePosition;
                }
            }
            return _queuePositionList[_queuePositionList.Count - 1];
        }
    }
}