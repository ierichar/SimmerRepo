using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;

namespace Simmer.NPC
{
    public class GiftSlotGroupManager : MonoBehaviour
    {
        private List<SpawningSlotManager> _itemSlotManagerList
            = new List<SpawningSlotManager>();

        public void Construct(ItemFactory itemFactory)
        {
            // Will get them in order of Scene Hierarchy from top to bottom
            SpawningSlotManager[] itemSlotManagerArray
                = GetComponentsInChildren<SpawningSlotManager>();

            for (int i = 0; i < itemSlotManagerArray.Length; ++i)
            {
                SpawningSlotManager thisSlot = itemSlotManagerArray[i];

                _itemSlotManagerList.Add(thisSlot);
                thisSlot.Construct(itemFactory, i);
            }
        }

        public void ClearAll()
        {
            foreach(var itemSlot in _itemSlotManagerList)
            {
                itemSlot.EmptySlot();
            }
        }

        public List<FoodItem> GetFoodItems()
        {
            List<FoodItem> result = new List<FoodItem>();

            foreach(var itemSlot in _itemSlotManagerList)
            {
                if(itemSlot.currentItem != null)
                {
                    result.Add(itemSlot.currentItem.foodItem);
                }
            }

            return result;
        }
    }
}