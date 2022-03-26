using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using Simmer.UI;

namespace Simmer.Items
{
    public class ItemFactory : MonoBehaviour
    {
        /// <summary>
        /// Prefab for ItemBehaviour
        /// </summary> 
        [SerializeField] private GameObject templateItem;
        /// <summary>
        /// Item move tween ease
        /// </summary> 
        public Ease moveEase;
        /// <summary>
        /// Minimum duration of move tween
        /// </summary> 
        public float minMoveDuration;
        /// <summary>
        /// Maximum duration of move tween
        /// </summary> 
        public float maxMoveDuration;
        /// <summary>
        /// If move distance is below this, move duration set to min 
        /// </summary> 
        public float minMoveDistance;
        /// <summary>
        /// If move distance is above this, move duration set to max 
        /// </summary> 
        public float maxMoveDistance;

        private PlayCanvasManager _playCanvasManager;

        public void Construct(PlayCanvasManager playCanvasManager)
        {
            _playCanvasManager = playCanvasManager;
        }

        /// <summary>
        /// Instantiates, constructs, and returns a new ItemBehaviour
        /// with foodItem data and itemSlotManager
        /// as ItemBehaviour.currentSlot
        /// </summary> 
        public ItemBehaviour ConstructItem(FoodItem foodItem
            , ItemSlotManager itemSlotManager)
        {
            GameObject newItem = Instantiate(templateItem
                , Vector3.zero, Quaternion.identity
                , itemSlotManager.rectTransform);

            ItemBehaviour newItemBehaviour =
                newItem.GetComponent<ItemBehaviour>();
            newItemBehaviour.Construct(_playCanvasManager
                , foodItem, itemSlotManager);

            return newItemBehaviour;
        }
    }
}