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
    /// <summary>
    /// Defines and controls all NPC interaction
    /// , VN_Manager interaction , UI function calls, and updating
    /// of NPC related GlobalPlayerData.
    /// </summary>
    /// TODO This class does too many different things;
    /// Maybe move quest related to subordinate class
    public class NPC_Manager : MonoBehaviour
    {
        // Dependency references
        /// <summary>
        /// VN_Manager controlling the NPC dialogue in this scene.
        /// </summary>
        public VN_Manager vn_manager { get; private set; }
        /// <summary>
        /// Reference to vn_manager.sharedVariables.
        /// </summary>
        public VN_SharedVariables vn_sharedVariables { get; private set; }
        /// <summary>
        /// Canvas manager for the market scene which includes
        /// the NPC interface windows.
        /// </summary>
        public MarketCanvasManager marketCanvasManager { get; private set; }
        private CanvasGroupManager _playCanvasGroupManager;
        private GameEventManager _gameEventManager;

        // Serailized variables
        /// <summary>
        /// Duration of fade in and out of play UI
        /// </summary>
        [SerializeField] private float _playCanvasFadeDuration;
        /// <summary>
        /// Easing of fade in and out of play UI
        /// </summary>
        [SerializeField] private Ease _playCanvasFadeEase;

        // Internal references
        private NPC_Shop _npcShop;
        private NPC_Gift _npcGift;
        private List<NPC_Behaviour> _allNPCList =
            new List<NPC_Behaviour>();

        // Class UnityEvents
        /// <summary>
        /// Invoked on player raycast interact by the associated
        /// NPC_Behaviour which passes its NPC_Data.
        /// </summary>
        public UnityEvent<NPC_Data> onNPCInteract
            = new UnityEvent<NPC_Data>();
        /// <summary>
        /// Invoked when the open NPC_InterfaceWindow is fully closed.
        /// </summary>
        public UnityEvent onCloseInterfaceCompleted
            = new UnityEvent();
        /// <summary>
        /// Invoked when a gift is given. True for correct
        /// , false for incorrect.
        /// </summary>
        public UnityEvent<bool> onTryGift
            = new UnityEvent<bool>();

        // State variables
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
            vn_manager.OnEndStory.AddListener(OnStopNPCInteractCallback);
            onTryGift.AddListener(OnTryGiftCallback);

            VN_EventData closeInterfaceData =
                new VN_EventData(onCloseInterfaceCompleted
                , "CloseComplete");
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
            // Set internal state
            _isInteracting = true;
            currentNPC_Data = npcData;
            
            //@ierichar 04/18/2022
            //currentNPC_Data.numOfInteractions++;
            vn_sharedVariables.interactionCount = npcData.numOfInteractions;
            vn_sharedVariables.isQuestStarted = npcData.isQuestStarted;

            if (npcData.characterData.name == "Taylor") UpdateVeggieFarmerQuest();
            if (npcData.characterData.name == "Missak") UpdateButcherQuest();
            if (npcData.characterData.name == "Bonnie") UpdateCowRancherQuest();
            if (npcData.characterData.name == "Mary") UpdateChickenKeeperQuest();

            Tween fadeTween = _playCanvasGroupManager.Fade(0,
                _playCanvasFadeDuration, _playCanvasFadeEase);
            yield return fadeTween.WaitForCompletion();

            marketCanvasManager.gameObject.SetActive(false);

            vn_manager.inkJSONAsset = npcData.npcInkAsset;
            vn_manager.StartStory();
            yield return null;
        }

        private void TrackQuest(NPC_Data npcData)
        {
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
                // If player already completed,
                // get data on this completed quest
                if (GlobalPlayerData.completedQuestDictionary
                    .ContainsKey(currentNPC_Data))
                {
                    UpdateQuestSharedVariables(false);
                }
                // If player not completed quest, add new quest
                else
                {
                    AddNewQuest(npcData);
                }
            }
            else
            {
                UpdateQuestSharedVariables(true);
            }
        }

        private void AddNewQuest(NPC_Data npcData)
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

        private void OnStopNPCInteractCallback()
        {
            StartCoroutine(StopInteractSequence());
        }

        private IEnumerator StopInteractSequence()
        {
            // @ierichar
            // Automatic check after interacting with an NPC to check
            // if the stage needs to progress
            Debug.Log("Talking to: " + currentNPC_Data.characterData.name);
            if (currentNPC_Data.characterData.name == "Taylor") UpdateVeggieFarmerQuest();
            if (currentNPC_Data.characterData.name == "Missak") UpdateButcherQuest();
            if (currentNPC_Data.characterData.name == "Bonnie") UpdateCowRancherQuest();
            if (currentNPC_Data.characterData.name == "Mary") UpdateChickenKeeperQuest();

            UpdateInteractionSharedVariables();
            UpdateStageSharedVariables();

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

        /// <summary>
        /// Updates vn_sharedVariables for currentNPC_Quest
        /// depending on isQuestOngoing.
        /// </summary>
        /// <param name="isQuestOngoing">
        /// True if quest already completed, false if not.
        /// </param>
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

        /// @ierichar
        /// <summary>
        /// Update vn_sharedVariables isQuestStarted flag
        /// </summary>
        private void UpdateQuestStartedSharedVariables(bool isQuestStarted) 
        {
            currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;
            vn_sharedVariables.isQuestStarted = 0;
        }

        /// @ierichar
        /// <summary>
        /// Update vn_sharedVariables interactionCount
        /// </summary>
        private void UpdateInteractionSharedVariables() 
        {
            currentNPC_Data.numOfInteractions = ++vn_sharedVariables.interactionCount;
            vn_sharedVariables.interactionCount = 0;
        }

        /// @ierichar
        /// <summary>
        /// Update Taylor (Veggie Farmer) quest progress
        /// </summary>
        private void UpdateVeggieFarmerQuest()
        {
            // Stage 0
            // Start quest with first interaction
            if (vn_sharedVariables.currentStage == 0 && vn_sharedVariables.isQuestStarted != 1) 
            {
                vn_sharedVariables.isQuestStarted = 1;
                currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;
                TrackQuest(currentNPC_Data);
            }
            // If quest is done, add yeast to shop
            
            // Stage 1
        }

        /// @ierichar
        /// <summary>
        /// Update Missak (Butcher) quest progress
        /// </summary>
        private void UpdateButcherQuest()
        {
            // Stage 0
            // Character needs to introduce themselves to everyone before talking
            // Stage 1
            if (vn_sharedVariables.currentStage == 1 && vn_sharedVariables.isQuestStarted != 1)
            {
                // Talk with him at least 2 times
                if (currentNPC_Data.numOfInteractions > 2) {
                    vn_sharedVariables.isQuestStarted = 1;
                    currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;
                    TrackQuest(currentNPC_Data);
                }
            }
        }

        /// @ierichar
        /// <summary>
        /// Update Bonnie (Cow Rancher) quest progress
        /// </summary>
        private void UpdateCowRancherQuest()
        {
            // Stage 0
            // Stage 1
            if (vn_sharedVariables.currentStage == 1 && vn_sharedVariables.isQuestStarted != 1) 
            {
                vn_sharedVariables.isQuestStarted = 1;
                TrackQuest(currentNPC_Data);
            }
        }

        /// @ierichar
        /// <summary>
        /// Update Mary (Chicken Keeper) quest progress
        /// </summary>
        private void UpdateChickenKeeperQuest()
        {
            // Stage 0
            // Stage 1
            if (vn_sharedVariables.currentStage == 1 && vn_sharedVariables.isQuestStarted != 1) 
            {
                vn_sharedVariables.isQuestStarted = 1;
                TrackQuest(currentNPC_Data);
            }
        }

        /// @ierichar
        /// <summary>
        /// Updates vn_sharedVariables based on currentStage
        /// </summary>
        private void UpdateStageSharedVariables() 
        {
            // Stage 0 to 1 Condition:
            //  - Stage 0
            //  - Meet all NPCs (numOfInteraction > 0)
            if(GlobalPlayerData.stageValue == 0)
            {
                bool metAll = true;
                foreach(NPC_Behaviour data in _allNPCList)
                {
                    if(data.GetNPC_Data().numOfInteractions == 0) 
                    {
                        metAll = false;
                        break;
                    }
                }
                if(metAll) 
                {
                    GlobalPlayerData.stageValue++;      // Update Global stage
                    foreach(NPC_Behaviour data in _allNPCList)
                    {
                        // Update each NPC's stage variable
                        data._npcManager.vn_sharedVariables.currentStage = 1;
                    }
                }
            }
            Debug.Log("Current Stage: " + GlobalPlayerData.stageValue);
        }
    }
}