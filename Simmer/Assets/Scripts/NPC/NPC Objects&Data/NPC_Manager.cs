using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

using Simmer.VN;
using Simmer.UI;
using Simmer.UI.NPC;
using Simmer.FoodData;
using Simmer.CustomTime;

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

            // --------------------------------------------------------
            // Shared Variables and NPC Data 
            // --------------------------------------------------------
            currentNPC_Data = npcData;

            Debug.Log("Start talking to: " + currentNPC_Data
                .characterData.name);

            // @ierichar 04/18/2022
            // @ierichar 05/06/2022 load vn_sharedvariables from stored NPC data
            LoadSharedVariables_from_NPC_Data(currentNPC_Data);

            // @ierichar 05/04/2022
            // New Track and Add Quest functionality
            TrackQuest_v2(currentNPC_Data);

            //vn_sharedVariables.interactionCount++;
            
            // @ierichar 05/06/2022
            // Commenting out to test Track and Add v2's
            //vn_sharedVariables.isQuestStarted = currentNPC_Data.isQuestStarted;
            // if (currentNPC_Data.isQuestStarted > 0) 
            // {
            //     TrackQuest(currentNPC_Data);
            // }
            // --------------------------------------------------------

            Tween fadeTween = _playCanvasGroupManager.Fade(0,
                _playCanvasFadeDuration, _playCanvasFadeEase);
            yield return fadeTween.WaitForCompletion();

            marketCanvasManager.gameObject.SetActive(false);

            vn_manager.inkJSONAsset = npcData.npcInkAsset;
            vn_manager.StartStory();
            yield return null;
        }

        // Evan's Code for Adding and Tracking quest data
        // 
        // private void TrackQuest(NPC_Data npcData)
        // {
        //     if (GlobalPlayerData.activeQuestDictionary
        //         .ContainsKey(currentNPC_Data))
        //     {
        //         // Store active quest for this NPC
        //         currentNPC_Quest = GlobalPlayerData
        //             .activeQuestDictionary[currentNPC_Data];
        //     }
        //     else currentNPC_Quest = null;

        //     if (currentNPC_Quest == null)
        //     {
        //         // If player already completed,
        //         // get data on this completed quest
        //         if (GlobalPlayerData.completedQuestDictionary
        //             .ContainsKey(currentNPC_Data))
        //         {
        //             UpdateSharedVariables(false);
        //         }
        //         // If player not completed quest, add new quest
        //         else
        //         {
        //             AddNewQuest(npcData);
        //         }
        //     }
        //     else
        //     {
        //         UpdateSharedVariables(true);
        //     }
        // }

        // private void AddNewQuest(NPC_Data npcData)
        // {
        //     currentNPC_Quest = npcData.questDataList[0];
        //     GlobalPlayerData.AddNewQuest(npcData
        //         , npcData.questDataList[0]);

        //     foreach (IngredientData item in currentNPC_Quest.initialKnowledge)
        //     {
        //         if (GlobalPlayerData.AddIngredientKnowledge(item))
        //             _newKnowledgeToAdd.Add(item);
        //     }

        //     UpdateQuestSharedVariables(true);
        // }

        // @ierichar
        // Prototype TrackQuest_v2()
        // Pre:  NPC_Data npcData - the npc who's quest we are tracking
        // Tracks the current quest given the NPC. Updates npc accordingly
        // if they have an active quest.
        private void TrackQuest_v2(NPC_Data currentNPC_Data)
        {
            Debug.Log("calling TrackQuest_v2...");
            
            // Call appropriate update quest for npc
            // and will AddQuest_v2 if prereqs are met
            Debug.Log("v2: isQuestStarted" + currentNPC_Data.characterData.name);
            switch (currentNPC_Data.characterData.name) 
            {
                case "Taylor":
                    UpdateVeggieFarmerQuest(currentNPC_Data);
                    break;
                case "Missak":
                    UpdateButcherQuest(currentNPC_Data);
                    break;
                case "Bonnie":
                    UpdateCowRancherQuest(currentNPC_Data);
                    break;
                case "Mary":
                    UpdateChickenKeeperQuest(currentNPC_Data);
                    break;
                default:
                    Debug.Log("TrackQuest_v2 : npcData.characterData.name ERROR");
                    break;
            }


            // If the quest has not been initialized or added beforehand
            if (GlobalPlayerData.activeQuestDictionary
                .ContainsKey(currentNPC_Data))
            {
                Debug.Log("TrackQuest_v2 : AQD trigger");
                // Store active quest for this NPC
                currentNPC_Quest = GlobalPlayerData
                    .activeQuestDictionary[currentNPC_Data];

            }

            // Add to complete quest list is
            if (currentNPC_Quest != null && currentNPC_Quest.isQuestComplete)
            {
                Debug.Log("TrackQuest_v2 : CQL trigger");
                vn_sharedVariables.isQuestComplete = 1;
                GlobalPlayerData.completedQuestList.Add(currentNPC_Quest);
                vn_sharedVariables.isQuestStarted = 0;
            }
        }

        // @ierichar
        // Prototype AddNewQuest_v2()
        // Pre:  NPC_Data npcData - the npc who's quest we are adding
        //       int questNumber  - the quest number in accessible quests in NPC_Data
        // Accepts NPC_Data then checks to make sure the npc and quest exists and the quest
        // that is trying 
        private void AddNewQuest_v2(NPC_Data currentNPC_Data, int questNumber)
        {
            Debug.Log("AddNewQuest_v2 adding: " + currentNPC_Data.questDataList[questNumber]);
            // Adding predetermined quest from prefab list
            currentNPC_Quest = currentNPC_Data.questDataList[questNumber];

            // Refer back to GPD.AddNewQuest
            GlobalPlayerData.AddNewQuest(currentNPC_Data, currentNPC_Data.questDataList[questNumber]);

            // Add ingredient knowledge from quest
            foreach (IngredientData item in currentNPC_Quest.initialKnowledge)
            {
                if (GlobalPlayerData.AddIngredientKnowledge(item))
                    _newKnowledgeToAdd.Add(item);
            }

            // Update shared variables when adding new quest
            //  - isQuestStarted is maintained in Update[NPC Name]Quest()
            //    before this function is called
            vn_sharedVariables.isQuestComplete = 0;
            vn_sharedVariables.questItem = currentNPC_Quest.questItem.name;
            vn_sharedVariables.questReward = currentNPC_Quest.questReward.name;
        }

        private void OnStopNPCInteractCallback()
        {
            StartCoroutine(StopInteractSequence());
        }

        private IEnumerator StopInteractSequence()
        {
            // --------------------------------------------------------
            // Shared Variables and NPC Data 
            // --------------------------------------------------------
            // @ierichar
            // 05/06/2022
            // Automatic check after interacting with an NPC to check
            // if the stage needs to progress
            Debug.Log("Done talking to: " + currentNPC_Data
                .characterData.name);

            // UpdateSharedVariables(false);
            TrackQuest_v2(currentNPC_Data);
            StoreSharedVariables_to_NPC_Data(currentNPC_Data);
            UpdateStageSharedVariables();
            // --------------------------------------------------------

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
        // private void UpdateSharedVariables(bool isQuestOngoing)
        // {
        //     if (isQuestOngoing)
        //     {

        //         vn_sharedVariables.isQuestComplete = 0;
        //         vn_sharedVariables.questItem = currentNPC_Quest
        //             .questItem.name;
        //         vn_sharedVariables.questReward = currentNPC_Quest
        //             .questReward.name;
        //     }
        //     else
        //     {
        //         vn_sharedVariables.isQuestComplete = 1;
        //         vn_sharedVariables.questItem = GlobalPlayerData
        //             .completedQuestDictionary[currentNPC_Data]
        //             .questItem.name;
        //         vn_sharedVariables.questReward = GlobalPlayerData
        //             .completedQuestDictionary[currentNPC_Data]
        //             .questReward.name;
        //     }

        //     // Update vn_sharedVariables isQuestStarted flag
        //     currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;
        //     vn_sharedVariables.isQuestStarted = 0;

        //     // Update vn_sharedVariables interactionCount
        //     currentNPC_Data.numOfInteractions = ++vn_sharedVariables.interactionCount;
        //     vn_sharedVariables.interactionCount = 0;
        // }

        /// @ierichar
        /// <summary>
        /// Loads the NPC_Data to vn_sharedvariables
        /// </summary>
        private void LoadSharedVariables_from_NPC_Data(NPC_Data currentNPC_Data) {
            // Load vn_sharedVariables isQuestStarted flag
            vn_sharedVariables.isQuestStarted = currentNPC_Data.isQuestStarted;

            // Load vn_sharedVariables interactionCount
            vn_sharedVariables.interactionCount = currentNPC_Data.numOfInteractions;

            // Load currentStage from GlobalPlayerData
            vn_sharedVariables.currentStage = GlobalPlayerData.stageValue;
        }

        /// @ierichar
        /// <summary>
        /// Stores the vn_sharedvariables to NPC_Data
        /// </summary>
        private void StoreSharedVariables_to_NPC_Data(NPC_Data currentNPC_Data) {
            // Store vn_sharedVariables isQuestStarted flag
            currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;
            vn_sharedVariables.isQuestStarted = 0;

            // Store vn_sharedVariables interactionCount
            // Increment count to indicate an interaciton was had
            currentNPC_Data.numOfInteractions = ++vn_sharedVariables.interactionCount;
            vn_sharedVariables.interactionCount = 0;
        }

        /// @ierichar
        /// <summary>
        /// Update Taylor (Veggie Farmer) quest progress
        /// </summary>
        /// <param name="currentNPC_Data">
        /// Passing current npc being interacted with.
        /// </param>
        private void UpdateVeggieFarmerQuest(NPC_Data currentNPC_Data)
        {
            Debug.Log("v2 : calling UpdateVeggieFarmerQuest...");

            // Stage 0 / 1
            // Start quest with first interaction
            if (vn_sharedVariables.currentStage >= 0 && vn_sharedVariables.isQuestStarted < 2) 
            {
                // Update isQuestStarted tracking for dialogue
                vn_sharedVariables.isQuestStarted = ++vn_sharedVariables.isQuestStarted;
                currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;

                // Prevent duplicate calls to track quest
                if (vn_sharedVariables.isQuestStarted == 1)
                {
                    AddNewQuest_v2(currentNPC_Data, 0);
                    Debug.Log("QuestItem is " + vn_sharedVariables.questItem);
                    Debug.Log("QuestItem" + vn_sharedVariables.questReward);
                }
            }
            
            // Stage 2
            // Uncomment once quest added to VeggieFarmer scriptable object
            // if (vn_sharedVariables.currentStage == 2 && vn_sharedVariables.isQuestStarted < 2) {
            //     // Update isQuestStarted tracking for dialogue
            //     vn_sharedVariables.isQuestStarted = ++vn_sharedVariables.isQuestStarted;
            //     currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;

            //     // Prevent duplicate calls to track quest
            //     if (vn_sharedVariables.isQuestStarted == 1)
            //     {
            //         AddNewQuest_v2(currentNPC_Data, 1);
            //         Debug.Log("QuestItem is " + vn_sharedVariables.questItem);
            //         Debug.Log("QuestItem" + vn_sharedVariables.questReward);
            //     }
            // }
        }

        /// @ierichar
        /// <summary>
        /// Update Missak (Butcher) quest progress
        /// </summary>
        /// <param name="currentNPC_Data">
        /// Passing current npc being interacted with.
        /// </param>
        private void UpdateButcherQuest(NPC_Data currentNPC_Data)
        {
            Debug.Log("v2 : calling UpdateButcherFarmerQuest...");

            // Stage 0
            // Character needs to introduce themselves to everyone before recieving quest

            // Stage 1
            if (vn_sharedVariables.currentStage >= 1 
                && vn_sharedVariables.isQuestStarted < 1)
            {
                // Check all npcs to find Bonnie
                foreach (NPC_Behaviour data in _allNPCList)
                {
                    // Check if Bonnie's quest is completete
                    if (data.GetNPC_Data().characterData.name == "Bonnie")
                    {
                        if (data.GetNPC_Data().questDataList[0].isQuestComplete)
                        {
                            // Update NPCs shared variable for isQuestStarted
                            vn_sharedVariables.isQuestStarted = ++vn_sharedVariables.isQuestStarted;
                            currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;

                            // Prevent duplicate calls to track quest
                            if (vn_sharedVariables.isQuestStarted == 1)
                            {
                                AddNewQuest_v2(currentNPC_Data, 0);
                            }
                        }
                    }
                }
            }

            // Stage 2
            // Uncomment once quest added to VeggieFarmer scriptable object
            // if (vn_sharedVariables.currentStage == 2 && vn_sharedVariables.isQuestStarted < 2) {
            //     // Update isQuestStarted tracking for dialogue
            //     vn_sharedVariables.isQuestStarted = ++vn_sharedVariables.isQuestStarted;
            //     currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;

            //     // Prevent duplicate calls to track quest
            //     if (vn_sharedVariables.isQuestStarted == 1)
            //     {
            //         AddNewQuest_v2(currentNPC_Data, 1);
            //         Debug.Log("QuestItem is " + vn_sharedVariables.questItem);
            //         Debug.Log("QuestItem" + vn_sharedVariables.questReward);
            //     }
            // }
        }

        /// @ierichar
        /// <summary>
        /// Update Bonnie (Cow Rancher) quest progress
        /// </summary>
        /// <param name="currentNPC_Data">
        /// Passing current npc being interacted with.
        /// </param>
        private void UpdateCowRancherQuest(NPC_Data currentNPC_Data)
        {
            Debug.Log("v2 : calling UpdateCowRancherQuest...");

            // Stage 0
            // Character needs to introduce themselves to everyone before recieving quest

            // Stage 1
            if (vn_sharedVariables.currentStage >= 1 && vn_sharedVariables.isQuestStarted < 2) 
            {
                // Talk with her at least 2 times
                if (currentNPC_Data.numOfInteractions > 2)
                {
                    vn_sharedVariables.isQuestStarted = ++vn_sharedVariables.isQuestStarted;
                    currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;

                    // Prevent duplicate calls to track quest
                    if (vn_sharedVariables.isQuestStarted == 1)
                    {
                        AddNewQuest_v2(currentNPC_Data, 0);
                    }
                }
            }

            // Stage 2
            // Uncomment once quest added to VeggieFarmer scriptable object
            // if (vn_sharedVariables.currentStage == 2 && vn_sharedVariables.isQuestStarted < 2) {
            //     // Update isQuestStarted tracking for dialogue
            //     vn_sharedVariables.isQuestStarted = ++vn_sharedVariables.isQuestStarted;
            //     currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;

            //     // Prevent duplicate calls to track quest
            //     if (vn_sharedVariables.isQuestStarted == 1)
            //     {
            //         AddNewQuest_v2(currentNPC_Data, 1);
            //         Debug.Log("QuestItem is " + vn_sharedVariables.questItem);
            //         Debug.Log("QuestItem" + vn_sharedVariables.questReward);
            //     }
            // }
        }

        /// @ierichar
        /// <summary>
        /// Update Mary (Chicken Keeper) quest progress
        /// </summary>
        /// <param name="currentNPC_Data">
        /// Passing current npc being interacted with.
        /// </param>
        private void UpdateChickenKeeperQuest(NPC_Data currentNPC_Data)
        {
            Debug.Log("v2 : calling UpdateChickenKeeperQuest...");
            // Stage 0
            // Character needs to introduce themselves to everyone before recieving quest

            // Stage 1
            if (vn_sharedVariables.currentStage >= 1 && vn_sharedVariables.isQuestStarted < 2) 
            {
                // Give it up for day 43!
                if (TimeManager.Day >= 1)
                {
                    vn_sharedVariables.isQuestStarted = ++vn_sharedVariables.isQuestStarted;
                    currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;

                    // Prevent duplicate calls to track quest
                    if (vn_sharedVariables.isQuestStarted == 1)
                    {
                        AddNewQuest_v2(currentNPC_Data, 0);
                    }
                }
            }

            // Stage 2
            // Uncomment once quest added to VeggieFarmer scriptable object
            // if (vn_sharedVariables.currentStage == 2 && vn_sharedVariables.isQuestStarted < 2) {
            //     // Update isQuestStarted tracking for dialogue
            //     vn_sharedVariables.isQuestStarted = ++vn_sharedVariables.isQuestStarted;
            //     currentNPC_Data.isQuestStarted = vn_sharedVariables.isQuestStarted;

            //     // Prevent duplicate calls to track quest
            //     if (vn_sharedVariables.isQuestStarted == 1)
            //     {
            //         AddNewQuest_v2(currentNPC_Data, 1);
            //         Debug.Log("QuestItem is " + vn_sharedVariables.questItem);
            //         Debug.Log("QuestItem" + vn_sharedVariables.questReward);
            //     }
            // }
        }

        /// @ierichar
        /// <summary>
        /// Updates vn_sharedVariables.currentStage based on 
        /// GlobalPlayerData stageValue
        /// </summary>
        private void UpdateStageSharedVariables() 
        {
            // Stage 0 to 1 Condition:
            //  - Stage 0
            //  - Meet all NPCs (numOfInteraction > 0)
            if (GlobalPlayerData.stageValue == 0)
            {
                bool metAll = true;
                foreach (NPC_Behaviour data in _allNPCList)
                {
                    // Check if all NPCs have been interacted with
                    if (data.GetNPC_Data().numOfInteractions == 0) 
                    {
                        metAll = false;
                        break;
                    }
                }
                if (metAll) 
                {
                    GlobalPlayerData.stageValue++;      // Update Global stage
                    foreach(NPC_Behaviour data in _allNPCList)
                    {
                        // Update each NPC's stage variable
                        data._npcManager.vn_sharedVariables.currentStage = 1;
                    }
                }
            }
            // Stage 1 to 2 Condition:
            //  - Stage 1
            //  - Complete all quests for NPCs
            if (GlobalPlayerData.stageValue == 1) {
                bool stage1QuestsComplete = true;
                foreach (NPC_Behaviour data in _allNPCList) 
                {
                    // Check via NPC_QuestData for each character
                    if (!data.GetNPC_Data().questDataList[0].isQuestComplete)
                    {
                        stage1QuestsComplete = false;
                    }

                }
                if (stage1QuestsComplete) {
                    GlobalPlayerData.stageValue++;      // Update Global stage
                    foreach (NPC_Behaviour data in _allNPCList)
                    {
                        // Update each NPC's stage variable
                        data._npcManager.vn_sharedVariables.currentStage++;
                    }
                }
            }
            Debug.Log("Current Stage: " + GlobalPlayerData.stageValue);
        }
    }
}