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

        public void SpawnFoodItem(FoodItem toSet)
        {
            SetNewSlot(_itemFactory.ConstructItem(toSet, this));
        }
    }
}