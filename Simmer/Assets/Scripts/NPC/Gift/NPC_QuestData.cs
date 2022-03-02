using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.NPC
{
    [CreateAssetMenu(fileName = "New NPC_QuestData"
            , menuName = "NPC/NPC_QuestData")]
    public class NPC_QuestData : ScriptableObject
    {
        public IngredientData questItem;

        public List<IngredientData> initialKnowledge;

        public IngredientData questReward;
    }
}