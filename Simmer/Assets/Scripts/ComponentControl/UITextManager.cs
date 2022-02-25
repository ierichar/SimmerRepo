using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Simmer.UI
{
    public class UITextManager : MonoBehaviour
    {
        public TextMeshProUGUI textMeshPro { get; private set; }

        public void Construct()
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text)
        {
            textMeshPro.text = text;
        }
    }
}