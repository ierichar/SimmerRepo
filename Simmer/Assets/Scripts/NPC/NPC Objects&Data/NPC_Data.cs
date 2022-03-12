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
        public Sprite characterSprite;

        public Sprite portraitSprite;

        public TextAsset npcInkAsset;

        public CharacterData characterData;

        public List<IngredientData> shopItemList = new List<IngredientData>();

        public List<IngredientData> likedItems = new List<IngredientData>();

        public List<NPC_QuestData> questDataList = new List<NPC_QuestData>();

        public Dictionary<IngredientData, IngredientData>
            questDictionary = new Dictionary<IngredientData, IngredientData>();

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