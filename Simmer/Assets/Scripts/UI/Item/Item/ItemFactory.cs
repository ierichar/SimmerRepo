using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.UI;

namespace Simmer.Items
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] private GameObject templateItem;

        private PlayCanvasManager _playCanvasManager;

        public void Construct(PlayCanvasManager playCanvasManager)
        {
            _playCanvasManager = playCanvasManager;
        }

        public ItemBehaviour ConstructItem(FoodItem foodItem, ItemSlotManager itemSlotManager)
        {
            GameObject newItem = Instantiate(templateItem
                , Vector3.zero, Quaternion.identity, itemSlotManager.rectTransform);
            ItemBehaviour newItemBehaviour = newItem.GetComponent<ItemBehaviour>();
            newItemBehaviour.Construct(_playCanvasManager, foodItem, itemSlotManager);

            return newItemBehaviour;
        }
    }
}