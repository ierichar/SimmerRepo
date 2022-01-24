using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.Items
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] private GameObject templateItem;

        private Canvas _playCanvas;

        public void Construct(Canvas playCanvas)
        {
            _playCanvas = playCanvas;
        }

        public ItemBehaviour ConstructItem(FoodItem foodItem, ItemSlotManager itemSlotManager)
        {
            GameObject newItem = Instantiate(templateItem
                , Vector3.zero, Quaternion.identity, itemSlotManager.rectTransform);
            ItemBehaviour newItemBehaviour = newItem.GetComponent<ItemBehaviour>();
            newItemBehaviour.Construct(_playCanvas, foodItem, itemSlotManager);

            return newItemBehaviour;
        }
    }
}