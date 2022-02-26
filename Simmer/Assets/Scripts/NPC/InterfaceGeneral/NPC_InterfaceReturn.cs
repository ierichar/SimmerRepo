using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.NPC
{
    public class NPC_InterfaceReturn : MonoBehaviour
    {
        private NPC_InterfaceWindow _interfaceWindow;
        private Button _returnButton;

        public void Construct(NPC_InterfaceWindow interfaceWindow)
        {
            _interfaceWindow = interfaceWindow;
            _returnButton = GetComponent<Button>();

            _returnButton.onClick.AddListener(OnReturnButtonClickCallback);
        }

        private void OnReturnButtonClickCallback()
        {
            _interfaceWindow.OnReturn.Invoke();
        }
    }
}