using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.NPC
{
    public class NPC_InterfaceExit : MonoBehaviour
    {
        private NPC_InterfaceWindow _interfaceWindow;
        private Button _exitButton;

        public void Construct(NPC_InterfaceWindow interfaceWindow)
        {
            _interfaceWindow = interfaceWindow;
            _exitButton = GetComponent<Button>();

            _exitButton.onClick.AddListener(OnExitButtonClickCallback);
        }

        private void OnExitButtonClickCallback()
        {
            _interfaceWindow.OnClose.Invoke();
        }
    }
}