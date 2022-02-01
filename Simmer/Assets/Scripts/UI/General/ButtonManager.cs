using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.UI
{
    public class ButtonManager : MonoBehaviour
    {
        //[SerializeField, SerializeReference]
        //private IControlUI controlUI;
        private Button _button;

        public void Construct()
        {
            _button = GetComponent<Button>();
            //_button.onClick.AddListener(controlUI.ToggleActive);
        }
    }
}