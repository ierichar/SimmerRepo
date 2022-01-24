using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simmer.Items
{
    public class ItemImageManager : MonoBehaviour
    {
        public Image image { get; private set; }

        public void Construct()
        {
            image = GetComponent<Image>();
        }

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }
    }
}