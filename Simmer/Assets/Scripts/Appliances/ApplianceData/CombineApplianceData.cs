using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.Appliance
{
    [CreateAssetMenu(fileName = "New CombineApplianceData"
        , menuName = "ApplianceData/CombineApplianceData")]

    public class CombineApplianceData : ApplianceData
    {
        public List<RecipeData> ingredientRecipeList
            = new List<RecipeData>();
    }
}