using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.Inventory
{
    public class ItemBackgroundManager : MonoBehaviour
    {
        public Image image { get; private set; }

        public void Construct()
        {
            image = GetComponent<Image>();
        }

        public void SetColor(Color color)
        {
            image.color = color;
        }
    }
}