using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI
{
    public class MoneyUI : MonoBehaviour
    {
        public UITextManager textManager { get; private set; }

        public void Construct()
        {
            textManager = GetComponentInChildren<UITextManager>();
            textManager.Construct();
        }
    }
}