using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapZoom : MonoBehaviour
    {
        [SerializeField] private float _scrollSensitivity;
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;

        private RectTransform _scaleTransform;

        private float currentScale = 1;

        public void Construct()
        {
            _scaleTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            ZoomInput();
        }

        private void ZoomInput()
        {
            if (Input.GetKey(KeyCode.Equals))
            {
                print("plus");
                currentScale += _scrollSensitivity;
                UpdateZoom();
            }
            if (Input.GetKey(KeyCode.Minus))
            {
                print("minus");
                currentScale -= _scrollSensitivity;
                UpdateZoom();
            }

            float scrollInput = Input.mouseScrollDelta.y * _scrollSensitivity;

            if(scrollInput != 0)
            {
                currentScale += scrollInput;
                UpdateZoom();
            }

        }

        private void UpdateZoom()
        {
            currentScale = Mathf.Clamp(currentScale, _minScale, _maxScale);

            _scaleTransform.localScale =
                new Vector3(currentScale, currentScale, 1);
        }
    }
}