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
            bool questCompleted = false;

            foreach(FoodItem foodItem in itemList)
            {
                IngredientData thisIngredient = foodItem.ingredientData;

                if (currentNPC_Data.questDictionary
                    .ContainsKey(thisIngredient))
                {
                    GlobalPlayerData.AddIngredientKnowledge(
                        currentNPC_Data.questDictionary[thisIngredient]);
                    questCompleted = true;
                }
                else
                {
                    print("Not quest item: " + thisIngredient.name);
                }
            }

            StartCoroutine(QuestCheckSequeunce(questCompleted));
        }

        private IEnumerator QuestCheckSequeunce(bool questCompleted)
        {
            _npcManager.onTryGift.Invoke(questCompleted);

            yield return StartCoroutine(giftReactionImage
                .ReactionSequence(questCompleted));

            if (questCompleted)
            {
                giftSlotGroupManager.ClearAll();

                OnClose.Invoke();
            }
        }

    }
}