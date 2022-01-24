using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Simmer.Items
{
    public class ItemCornerTextManager : MonoBehaviour
    {
        public TextMeshProUGUI textMeshPro { get; private set; }

        public void Construct(int index)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
            SetText((index + 1).ToString());
        }

        public void SetText(string text)
        {
            textMeshPro.text = text;
        }
    }
}