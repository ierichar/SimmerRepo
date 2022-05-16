using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.Items;
using Simmer.FoodData;
using Simmer.NPC;

namespace Simmer.NPC
{
    public class NPC_Gift : NPC_InterfaceWindow
    {
        public GiftSlotGroupManager giftSlotGroupManager { get; private set; }
        public GiftButton giftButton { get; private set; }
        public GiftReactionImage giftReactionImage { get; private set; }

        public UnityEvent<List<FoodItem>> onClickGift
            = new UnityEvent<List<FoodItem>>();

        public override void Construct(NPC_Manager npcManager)
        {
            _onChooseString = "ChooseGift";
            _onCloseString = "CloseGift";

            base.Construct(npcManager);

            giftSlotGroupManager = gameObject
                .FindChildObject<GiftSlotGroupManager>();
            giftSlotGroupManager.Construct(npcManager
                .marketCanvasManager.itemFactory);

            giftButton = gameObject.FindChildObject<GiftButton>();
            giftButton.Construct(this);

            giftReactionImage = gameObject.FindChildObject<GiftReactionImage>();
            giftReactionImage.Construct();

            onClickGift.AddListener(OnClickGiftCallback);
        }

        private void OnClickGiftCallback(List<FoodItem> itemList)
        {
            Debug.Log("calling OnClickGiftCallback with " + itemList);
            bool questCompleted = false;

            if (_npcManager.currentNPC_Quest != null)
            {
                questCompleted = TryCompleteQuest(itemList);
            }
            StartCoroutine(QuestCheckSequeunce(questCompleted));
        }

        private bool TryCompleteQuest(List<FoodItem> itemList)
        {
            Debug.Log("calling TryCompleteQuest with " + itemList);
            bool result = false;
            foreach (FoodItem foodItem in itemList)
            {
                IngredientData thisIngredient = foodItem.ingredientData;


                Debug.Log("Tried to complete quest with " + thisIngredient);
                Debug.Log("Compared to " + _npcManager.currentNPC_Quest.questItem);
                if (thisIngredient == _npcManager.currentNPC_Quest
                    .questItem)
                {
                    GlobalPlayerData.AddIngredientKnowledge(
                        currentNPC_Data.questDictionary[thisIngredient]);
                    GlobalPlayerData.CompleteQuest(_npcManager.currentNPC_Data
                        , _npcManager.currentNPC_Quest);

                    result = true;
                }
            }
            return result;
        }

        private IEnumerator QuestCheckSequeunce(bool questCompleted)
        {
            _npcManager.onTryGift.Invoke(questCompleted);

            yield return StartCoroutine(giftReactionImage
                .ReactionSequence(questCompleted));
            // small unplesent feeling, when spamming gift button after a successful gift
            // the following unsuccessful gift finishes first and calls releaseLock()
            // before the fade out is done, consider linking releaseLock()
            if (questCompleted)
            {
                giftSlotGroupManager.ClearAll();

                OnClose.Invoke();
                giftReactionImage.ReleaseLock();
            }
        }

    }
}