using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Simmer.UI.ImageQueue;
using Simmer.FoodData;
using Simmer.Items;
using Simmer.Appliance;
using Simmer.NPC;

public static class GlobalPlayerData
{
    public static Dictionary<int, FoodItem>
        foodItemDictionary
    {
        get { return _inventoryItemDictionary; }
        set { foodItemDictionary = _inventoryItemDictionary; }
    }
    private static Dictionary<int, FoodItem> _inventoryItemDictionary
        = new Dictionary<int, FoodItem>();

    public static Dictionary<ApplianceData, List<FoodItem>> AppInvSaveStruct = new Dictionary<ApplianceData, List<FoodItem>>();
    public static List<FoodItem> PantryInventory = new List<FoodItem>();

    public static List<IngredientData> knownIngredientList { get; private set; }

    public static int playerMoney { get; private set; }

    private static bool isConstructed = false;
    //@ierichar--------------------------------------------------------
    public static int stageValue = 0;
    //-----------------------------------------------------------------
    public static Dictionary<NPC_Data, NPC_QuestData> activeQuestDictionary
        = new Dictionary<NPC_Data, NPC_QuestData>();

    public static Dictionary<NPC_Data, NPC_QuestData> completedQuestDictionary
        = new Dictionary<NPC_Data, NPC_QuestData>();

    //@ierichar--------------------------------------------------------
    // Testing Add and Track v2 in NPC_Manager.cs
    public static List<NPC_QuestData> completedQuestList
        = new List<NPC_QuestData>();
    //-----------------------------------------------------------------

    public static UnityEvent OnActiveQuestsUpdated = new UnityEvent();

    public static UnityEvent<IngredientData>
        OnNewKnowledgeAdded
        = new UnityEvent<IngredientData>();

    //current time indices explained:
    //[0] = Hour   ,    [1] = minute   ,   [2] = AM/PM   ,   [3] = day   ,   [4] = paused?
    public static int[] currentTime = new int[5];

    //// 1. item to queue, 2. Origin location, 3. Desitination queue
    //public static UnityEvent<IngredientData, RectTransform, ImageQueueManager>
    //       OnNewQueueDispatch = new UnityEvent<IngredientData, RectTransform, ImageQueueManager>();

    public static void Construct(SaveData startingSaveData)
    {
        if (isConstructed) return;

        isConstructed = true;

        // @ierichar
        // Loading starting save data of stage : 0
        stageValue = startingSaveData.startingStage;

        knownIngredientList = new List<IngredientData>();

        if (startingSaveData == null)
        {
            Debug.Log("No startingSaveData given");
            return;
        }

        playerMoney = startingSaveData.startingMoney;

        for (int k=0; k<startingSaveData.startingPantry.Count; ++k){
            FoodItem item = new FoodItem(startingSaveData.startingPantry[k], null);
            PantryInventory.Add(item);
        }

        for (int i = 0; i < startingSaveData.startInventoryList.Count; ++i)
        {
            IngredientData ingredient = startingSaveData.startInventoryList[i];
            FoodItem startFoodItem = new FoodItem(ingredient, null);
            _inventoryItemDictionary.Add(i, startFoodItem);

            //AddIngredientKnowledge(ingredient);
        }

        foreach(IngredientData ingredient in startingSaveData.startKnownList)
        {
            AddIngredientKnowledge(ingredient);
        }

        //load startingTime
        currentTime[0] = startingSaveData.startingTime[0];
        currentTime[1] = startingSaveData.startingTime[1];
        currentTime[2] = startingSaveData.startingTime[2];
        currentTime[3] = startingSaveData.startingTime[3];
        currentTime[4] = 1;
    }

    public static void SaveInventoryDictionary(
        Dictionary<int, FoodItem> toSave)
    {
        _inventoryItemDictionary = toSave;

        foreach(var pair in _inventoryItemDictionary)
        {
            IngredientData thisIngredient = pair.Value.ingredientData;

            AddIngredientKnowledge(thisIngredient);
        }
    }

    public static bool AddIngredientKnowledge(
        IngredientData ingredient)
    {
        if (!knownIngredientList.Contains(ingredient))
        {
            knownIngredientList.Add(ingredient);
            //OnNewKnowledgeAdded.Invoke(ingredient);
            return true;
        }
        return false;
    }

    public static void SetMoney(int amount)
    {
        playerMoney = amount;
    }

    public static bool AddNewQuest(NPC_Data npcData
        , NPC_QuestData questData)
    {
        if (!activeQuestDictionary.ContainsKey(npcData))
        {
            activeQuestDictionary.Add(npcData, questData);

            // @ierichar
            questData.isQuestComplete = false;
            questData.isQuestStarted = true;

            OnActiveQuestsUpdated.Invoke();
            return true;
        }

        return false;
    }

    public static bool CompleteQuest(NPC_Data npcData
        , NPC_QuestData questData)
    {
        if (activeQuestDictionary.ContainsKey(npcData))
        {
            activeQuestDictionary.Remove(npcData);
            //completedQuestDictionary.Add(npcData, questData);
            // @ierichar
            // Added list functionality
            completedQuestList.Add(questData);
            questData.isQuestComplete = true;
            
            OnActiveQuestsUpdated.Invoke();
            return true;
        }

        return false;
    }

    public static void SaveCurrentTime(int hour, int minute, int AM, int Day, int paused){
        if(AM != 0 && AM != 1){
            Debug.LogError("AM must be 0 or 1 for true of false respectivly");
        }
        if(paused != 0 && paused != 1){
            Debug.LogError("Pause must be 0 or 1 for true of false respectivly");
        }
        currentTime[0] = hour;
        currentTime[1] = minute;
        currentTime[2] = AM;
        currentTime[3] = Day;
        currentTime[4] = paused;
    }
}