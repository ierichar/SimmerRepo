using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Simmer.UI;

namespace Simmer.Items
{
    public class SpawningSlotManager : ItemSlotManager, IDropHandler
    {
        protected ItemFactory _itemFactory;

        public void Construct(ItemFactory itemFactory
            , int index)
        {
            base.Construct(index);
            
            _itemFactory = itemFactory;
        }

        /// <summary>
        /// Spawns a new FoodItem on this slot using the
        /// ItemFactory.ConstructItem method.
        /// </summary>
        /// <param name="toSet">
        /// FoodItem data to be spawned on this item slot.
        /// </param>
        public void SpawnFoodItem(FoodItem toSet)
        {
            SetNewSlot(_itemFactory.ConstructItem(toSet, this));
        }
    }
}