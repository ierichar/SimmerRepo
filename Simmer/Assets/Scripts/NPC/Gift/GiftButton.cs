using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Simmer.Items;

namespace Simmer.NPC
{
    public class GiftButton : MonoBehaviour
    {
        private Button _button;

        private UnityEvent<List<FoodItem>> _onClickEvent;

        private GiftSlotGroupManager _giftSlotGroupManager;

        public void Construct(NPC_Gift npc_gift)
        {
            _onClickEvent = npc_gift.onClickGift;
            _giftSlotGroupManager = npc_gift.giftSlotGroupManager;

            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickCallback);
        }

        private void OnClickCallback()
        {
            _onClickEvent.Invoke(_giftSlotGroupManager.GetFoodItems());
        }
    }
}