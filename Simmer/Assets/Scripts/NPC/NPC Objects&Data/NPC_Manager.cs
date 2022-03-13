using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

using Simmer.VN;
using Simmer.UI;
using Simmer.UI.NPC;
using Simmer.FoodData;

namespace Simmer.NPC
{
    public class NPC_Manager : MonoBehaviour
    {
        public VN_Manager vn_manager { get; private set; }
        public VN_SharedVariables vn_sharedVariables { get; private set; }
        public MarketCanvasManager marketCanvasManager { get; private set; }
        private CanvasGroupManager _playCanvasGroupManager;
        private GameEventManager _gameEventManager;

        [SerializeField] private float _playCanvasFadeDuration;
        [SerializeField] private Ease _playCanvasFadeEase;

        private NPC_Shop _npcShop;
        private NPC_Gift _npcGift;

        private List<NPC_Behaviour> _allNPCList =
            new List<NPC_Behaviour>();

        public UnityEvent<NPC_Data> onNPCInteract
            = new UnityEvent<NPC_Data>();
        public UnityEvent onCloseInterfaceCompleted
            = new UnityEvent();
        public UnityEvent<bool> onTryGift
            = new UnityEvent<bool>();

        public NPC_InterfaceWindow targetInterfaceWindow;
        public NPC_Data currentNPC_Data { get; private set; }
        public NPC_QuestData currentNPC_Quest { get; private set; }

        private bool _isInteracting;
        private List<IngredientData> _newKnowledgeToAdd =
            new List<IngredientData>();

        public void Construct(VN_Manager VNmanager
            , MarketCanvasManager marketCanvasManager
            , GameEventManager gameEventManager)
        {
            vn_sharedVariables = VN_Util.manager.sharedVariables;
            _gameEventManager = gameEventManager;

            vn_manager = VNmanager;
            this.marketCanvasManager = marketCanvasManager;
            _playCanvasGroupManager = marketCanvasManager.canvasGroupManager;

            _npcShop = FindObjectOfType<NPC_Shop>(true);
            _npcShop.Construct(this);

            _npcGift = FindObjectOfType<NPC_Gift>(true);
            _npcGift.Construct(this);

            NPC_Behaviour[] npcArray = FindObjectsOfType<NPC_Behaviour>();
            foreach(var npc in npcArray)
            {
                _allNPCList.Add(npc);
                npc.Construct(this);
            }

            onNPCInteract.AddListener(OnNPCInteractCallback);
            vn_manager.OnEndStory.AddListener(OnStopNPCInteract);
            onTryGift.AddListener(OnTryGiftCallback);

            VN_EventData closeInterfaceData =
                new VN_EventData(onCloseInterfaceCompleted, "CloseComplete");
            vn_sharedVariables.AddEventData(closeInterfaceData);
        }

        public IEnumerator ShowInterfaceSequence()
        {
            targetInterfaceWindow.OnOpen.Invoke(currentNPC_Data);
            marketCanvasManager.gameObject.SetActive(true);

            Tween fadeTween = _playCanvasGroupManager.Fade(1,
                _playCanvasFadeDuration, _playCanvasFadeEase);
            yield return fadeTween.WaitForCompletion();
        }

        public IEnumerator HideInterfaceSequence()
        {
            Tween fadeTween = _playCanvasGroupManager.Fade(0,
                _playCanvasFadeDuration, _playCanvasFadeEase);
            yield return fadeTween.WaitForCompletion();

            targetInterfaceWindow.gameObject.SetActive(false);
            marketCanvasManager.gameObject.SetActive(false);
            
            targetInterfaceWindow = null;
            onCloseInterfaceCompleted.Invoke();
        }

        private void OnTryGiftCallback(bool isCorrect)
        {
            if(isCorrect) vn_sharedVariables.isCorrectGift = 1;
            else vn_sharedVariables.isCorrectGift = 0;
        }

        private void OnNPCInteractCallback(NPC_Data npcData)
        {
            if (!_isInteracting
                && vn_manager.state == VN_Manager.VN_State.end)
            {
                StartCoroutine(InteractSequence(npcData));
            }
        }

        private IEnumerator InteractSequence(NPC_Data npcData)
        {
            _isInteracting = true;
            currentNPC_Data = npcData;

            if (GlobalPlayerData.activeQuestDictionary
                .ContainsKey(currentNPC_Data))
            {
                // Store active quest for this NPC
                currentNPC_Quest = GlobalPlayerData
                    .activeQuestDictionary[currentNPC_Data];
            }
            else currentNPC_Quest = null;

            if (currentNPC_Quest == null)
            {
                // If player already completed, don't get new quest
                if(GlobalPlayerData.completedQuestDictionary
                    .ContainsKey(currentNPC_Data))
                {
                    UpdateQuestSharedVariables(false);
                }
                else
                {
                    currentNPC_Quest = npcData.questDataList[0];
                    GlobalPlayerData.AddNewQuest(npcData
                        , npcData.questDataList[0]);

                    foreach (IngredientData item in currentNPC_Quest.initialKnowledge)
                    {
                        if (GlobalPlayerData.AddIngredientKnowledge(item))
                            _newKnowledgeToAdd.Add(item);
                    }

                    UpdateQuestSharedVariables(true);
                }
            }
            else
            {
                UpdateQuestSharedVariables(true);
            }

            Tween fadeTween = _playCanvasGroupManager.Fade(0,
                _playCanvasFadeDuration, _playCanvasFadeEase);
            yield return fadeTween.WaitForCompletion();

            marketCanvasManager.gameObject.SetActive(false);

            vn_manager.inkJSONAsset = npcData.npcInkAsset;
            vn_manager.StartStory();
            yield return null;
        }

        private void OnStopNPCInteract()
        {
            StartCoroutine(StopInteractSequence());
        }

        private IEnumerator StopInteractSequence()
        {
            marketCanvasManager.gameObject.SetActive(true);

            Tween fadeTween = _playCanvasGroupManager.Fade(1,
                _playCanvasFadeDuration, _playCanvasFadeEase);
            yield return fadeTween.WaitForCompletion();

            _gameEventManager.onInteractUI.Invoke(false);
            currentNPC_Data = null;
            _isInteracting = false;

            foreach(IngredientData ingredient in _newKnowledgeToAdd)
            {
                marketCanvasManager.centerQueueTrigger
                    .npcKnowledgeTrigger.SpawnQueueItem(ingredient);
            }

            _newKnowledgeToAdd.Clear();
        }

        private void UpdateQuestSharedVariables(bool isQuestOngoing)
        {
            if(isQuestOngoing)
            {
                vn_sharedVariables.isQuestComplete = 0;
                vn_sharedVariables.questItem = currentNPC_Quest
                    .questItem.name;
                vn_sharedVariables.questReward = currentNPC_Quest
                    .questReward.name;
            }
            else
            {
                vn_sharedVariables.isQuestComplete = 1;
                vn_sharedVariables.questItem = GlobalPlayerData
                    .completedQuestDictionary[currentNPC_Data]
                    .questItem.name;
                vn_sharedVariables.questReward = GlobalPlayerData
                    .completedQuestDictionary[currentNPC_Data]
                    .questReward.name;
            }
            
        }
    }
}