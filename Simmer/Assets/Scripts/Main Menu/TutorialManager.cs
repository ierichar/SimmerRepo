using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.VN;
namespace Simmer.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        private VN_SharedVariables vn_sharedVariables;
        private RectTransform _rectTransform;

        [SerializeField] private List<TutorialSlide> _slidePrefabList
            = new List<TutorialSlide>();

        private UnityEvent OnNextSlide = new UnityEvent();

        private TutorialSlide _currentSlide;
        private int _slideIndex = 0;

        public void Construct()
        {
            vn_sharedVariables = VN_Util.manager.sharedVariables;

            _rectTransform = GetComponent<RectTransform>();

            OnNextSlide.AddListener(SpawnNextSlide);

            VN_EventData spawnNextSlideData =
                new VN_EventData(OnNextSlide, "NextSlide");
            vn_sharedVariables.AddEventData(spawnNextSlideData);

            SpawnNextSlide();
        }

        public void SpawnNextSlide()
        {
            if (_currentSlide != null) Destroy(_currentSlide.gameObject);
            if (_slideIndex >= _slidePrefabList.Count)
            {
                Debug.LogError(this + " Error: Cannot SpawnNextSlide beyond " +
                    "_slidePrefabList range");
                return;
            }

            print("Spawning: " + _slideIndex + _slidePrefabList[_slideIndex]);
             _currentSlide = Instantiate(
                _slidePrefabList[_slideIndex], gameObject.transform);

            _slideIndex++;
        }
    }
}