using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI
{
    public class ButtonGroup : MonoBehaviour
    {
        private List<ButtonManager> buttonManagerList
            = new List<ButtonManager>();


        public void Construct()
        {
            ButtonManager[] buttonManagerArray
                = GetComponentsInChildren<ButtonManager>();

            foreach (ButtonManager buttonManager in buttonManagerArray)
            {
                buttonManagerList.Add(buttonManager);
            }
        }
    }
}