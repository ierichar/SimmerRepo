using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Appliance;

namespace Simmer.FoodData
{
    [CreateAssetMenu(fileName = "New IngredientData"
        , menuName = "FoodData/IngredientData")]

    public class IngredientData : ScriptableObject
    {
        [Header("Base Variables")]
        public Sprite sprite;
        public int baseValue;
        public bool isFinalProduct;

        [SerializeField]
        private List<RecipeData> _recipeEdgeList
            = new List<RecipeData>();

        public Dictionary<ApplianceData, List<RecipeData>>
            applianceRecipeListDict
            = new Dictionary<ApplianceData, List<RecipeData>>();

        public enum VariantMode
        {
            Additive,
            BaseOnly
        }
        [Header("Variant Variables")]
        public VariantMode variantMode;
        public int minVariants;
        public int maxVariants;

        public List<IngredientLayer> ingredientLayerList
            = new List<IngredientLayer>();

        public Dictionary<IngredientData, IngredientLayer>
            expandLayerDict = new Dictionary<IngredientData, IngredientLayer>();

        private int _leafCount;

        private string layerDictDebug = "";

        private void OnValidate()
        {
            Construct();
        }

        private void Awake()
        {
            Construct();
        }

        private void Construct()
        {
            layerDictDebug = "";

            applianceRecipeListDict.Clear();
            foreach (RecipeData recipe in _recipeEdgeList)
            {
                ApplianceData thisAppliance = recipe.applianceData;
                if (recipe != null && recipe.applianceData)
                {
                    if (applianceRecipeListDict.ContainsKey(thisAppliance))
                    {
                        applianceRecipeListDict[thisAppliance].Add(recipe);
                    }
                    else
                    {
                        List<RecipeData> applianceRecipeList = new List<RecipeData>();
                        applianceRecipeList.Add(recipe);
                        applianceRecipeListDict.Add(recipe.applianceData, applianceRecipeList);
                    }
                }
            }

            expandLayerDict.Clear();

            IngredientLayer baseIngredientLayer
                = new IngredientLayer(this, null, 0);
            _leafCount = 0;
            RecursivePopulateLayerList(baseIngredientLayer);

            //Debug.Log(this + " " + layerDictDebug);
        }

        private void RecursivePopulateLayerList(IngredientLayer ingredientLayer)
        {
            List<IngredientLayer> parentLayerList = ingredientLayer
                .ingredientData.ingredientLayerList;

            if (parentLayerList.Count != 0)
            {
                List<IngredientLayer> childrenLayers
                    = GetChildrenLayers(ingredientLayer);

                for (int i = 0; i < childrenLayers.Count; ++i)
                {
                    IngredientLayer childLayer = childrenLayers[i];
                    RecursivePopulateLayerList(childLayer);     
                }
            }
            else
            {
                AddLayer(ingredientLayer);
            }
        }

        private List<IngredientLayer> GetChildrenLayers(IngredientLayer ingredientLayer)
        {
            List<IngredientLayer> childrenLayers
                    = ingredientLayer.ingredientData.ingredientLayerList;
            int childrenNum = childrenLayers.Count;

            childrenLayers.Sort(delegate (IngredientLayer x, IngredientLayer y)
            {
                if (x.layerNum == y.layerNum) return 0;
                else if (x.layerNum > y.layerNum) return 1;
                else return -1;
            });

            return childrenLayers;
        }

        private void AddLayer(IngredientLayer ingredientLayer)
        {
            IngredientLayer finalLayer = new IngredientLayer(
                    ingredientLayer.ingredientData, ingredientLayer.layerSprite
                    , _leafCount);
            if (expandLayerDict.ContainsKey(finalLayer.ingredientData))
            {
                Debug.LogError(this.name + " Error: ingredientData key \""
                    + finalLayer.ingredientData + "\" already in expandLayerDict");
            }
            else
            {
                expandLayerDict.Add(finalLayer.ingredientData, finalLayer);

                layerDictDebug += ingredientLayer.ingredientData.name + " "
                    + _leafCount + ", ";

                _leafCount++;
            }
        }
    }
}