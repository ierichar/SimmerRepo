using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.VN;
using Simmer.FoodData;
using Simmer.CustomTime;

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

        /// @ierichar
        /// <summary>
        /// Number of interactions player has with NPC
        /// </summary>
        public int numOfInteractions;

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

        public List<IngredientData> selectItemsBasedOnTime(int Day) {
            List<IngredientData> selectedItems = new List<IngredientData>();
            
            //Add code here to select a certain set of items based on how many should be shown at 1 time
            //The specific items  that show should be based on the current time from TimeManger
            /*
            int index = 0;
            if(Hour >= 6 && Hour<=9 && AM){
                index = 0;
            }else if(Hour > 9 && Hour<=11 && AM){
                index = shopItemList.Count/3;
            }else{
                index = shopItemList.Count/3 * 2;
                Debug.Log("Index: " + index);
            }
            Debug.Log("shopList count: " + shopItemList.Count);
            for(int i=0; i < 3; i++){
                Debug.Log("i: " + (index+i));
                selectedItems.Add(shopItemList[(index+i)%(shopItemList.Count)]);
            }
            return selectedItems;
            */

            int initial = (Day-1) % shopItemList.Count;
            for(int i=0; i < 3; i++){
                //Debug.Log("i: " + (initial+i));
                selectedItems.Add(shopItemList[(initial+i)%(shopItemList.Count)]);
            }
            return selectedItems;
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

            // @ierichar
            numOfInteractions = 0;

            foreach (NPC_QuestData questData in questDataList)
            {
                questDictionary.Add(questData.questItem
                    , questData.questReward);
            }
        }
    }
}