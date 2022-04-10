using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.VN;
using Simmer.FoodData;

namespace Simmer.NPC
{
    [CreateAssetMenu(fileName = "New NPC_Data"
            , menuName = "NPC/NPC_Data")]
    public class NPC_Data : ScriptableObject
    {
        /// <summary>
        /// Sprite of character in world space.
        /// </summary>
        public Sprite characterSprite;

        /// <summary>
        /// Sprite of character in UI space.
        /// </summary>
        public Sprite portraitSprite;

        /// <summary>
        /// Compiled .json file of an Ink story with Google Drive
        /// "TEMPLATE Generic NPC Interaction" story template.
        /// Defines all dialogue.
        /// </summary>
        public TextAsset npcInkAsset;

        /// <summary>
        /// Associated Simmer.VN CharacterData
        /// (see documentation for more)
        /// </summary>
        public CharacterData characterData;

        /// <summary>
        /// List of all possible items that appear in NPC's shop
        /// </summary>
        public List<IngredientData> shopItemList = new List<IngredientData>();

        /// <summary>
        /// List of items that the player can sell to
        /// the NPC for double value
        /// </summary>
        public List<IngredientData> likedItems = new List<IngredientData>();

        /// <summary>
        /// List of all quests. (currently only supports 1 quest)
        /// </summary>
        public List<NPC_QuestData> questDataList = new List<NPC_QuestData>();

        /// <summary>
        /// Quick access dictionary of quest data with
        /// key questItem and value questReward
        /// </summary>
        public Dictionary<IngredientData, IngredientData>
            questDictionary = new Dictionary<IngredientData, IngredientData>();

        /// <summary>
        /// Returns a list of size numToSelect
        /// composed of random items from shopItemList
        /// </summary>
        public List<IngredientData> selectRandom(int numToSelect) {
            List<IngredientData> selectedItem = new List<IngredientData>();
            for(int i = 0; i < numToSelect; i++) {
                selectedItem.Add(shopItemList[Random.Range(0, shopItemList.Count)]);
            }
            return selectedItem;
        }

        public void Awake()
        {
            Construct();
        }

        public void OnValidate()
        {
            Construct();
        }

        public void Construct()
        {
            questDictionary.Clear();

            foreach (NPC_QuestData questData in questDataList)
            {
                questDictionary.Add(questData.questItem
                    , questData.questReward);
            }
        }
    }
}