
using UnityEngine;

using Simmer.FoodData;
using Simmer.Items;
using Simmer.Appliance;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapGenerator : MonoBehaviour
    {
        [SerializeField] private RectTransform apexPositionMarker;
        private RecipeMapManager _recipeMapManager;
        private IngredientNodeFactory _ingredientNodeFactory;
        private TextNodeFactory _textNodeFactory;
        private EdgeLineFactory _edgeLineFactory;
        private TreeNodePositioning _treeNodePositioning;
        private AllFoodData _allFoodData;

        [SerializeField] private IngredientData _apexIngredient;
        [SerializeField] private float verticalSpacing;
        [SerializeField] private float verticalLineGap;

        public void Construct(RecipeMapManager recipeMapManager)
        {
            _recipeMapManager = recipeMapManager;
            _ingredientNodeFactory = recipeMapManager.ingredientNodeFactory;
            _textNodeFactory = recipeMapManager.textNodeFactory;
            _edgeLineFactory = recipeMapManager.edgeLineFactory;
            _treeNodePositioning = recipeMapManager.treeNodePositioning;
            _allFoodData = recipeMapManager.allFoodData;

            _recipeMapManager.recipeMapEventManager
                .OnShowRecipeMap.AddListener(OnShowRecipeMapCallback);
            _recipeMapManager.recipeMapEventManager
                .OnShowUtilityMap.AddListener(OnShowUtilityMapCallback);
        }

        private void OnShowRecipeMapCallback(IngredientData ingredientData)
        {
            RenderRecipeMap(ingredientData);
        }

        private void OnShowUtilityMapCallback(IngredientData ingredientData)
        {
            RenderUtilityMap(ingredientData);
        }

        public void RenderRecipeMap(IngredientData apexIngredient)
        {
            _ingredientNodeFactory.ClearAll();
            _edgeLineFactory.ClearAll();

            IngredientTree apexTree =
                    _treeNodePositioning.SpawnRecipeMap(apexIngredient);

            RenderRecipeMapRecursive(apexTree);

            CenterTree(apexTree, false);
        }

        public void RenderUtilityMap(IngredientData apexIngredient)
        {
            _ingredientNodeFactory.ClearAll();
            _edgeLineFactory.ClearAll();

            IngredientTree apexTree =
                    _treeNodePositioning.SpawnUtilityMap(apexIngredient);

            RenderUtilityMapRecursive(apexTree);

            CenterTree(apexTree, true);
        }

        private void CenterTree(IngredientTree apexTree, bool isTopDown)
        {
            // Center on apexPositionMarker
            Vector2 oldApexPosition
                = new Vector2(apexTree.xPosition, apexTree.yPosition);
            Vector2 displacement = apexPositionMarker
                .anchoredPosition - oldApexPosition;

            // Center tree vertical midpoint on apexPositionMarker
            float maxDepth = _treeNodePositioning
                .GetLowestDepth(apexTree, 0);
            float yDisplacement = (apexTree.yPosition - maxDepth) / 2;

            if(isTopDown)
            {
                displacement -= new Vector2(0, yDisplacement * verticalSpacing);
            }
            else
            {
                displacement += new Vector2(0, yDisplacement * verticalSpacing);
            }

            _ingredientNodeFactory.SetPosition(Vector2.zero);
            _ingredientNodeFactory.Displace(displacement);

            _edgeLineFactory.SetPosition(Vector2.zero);
            _edgeLineFactory.Displace(displacement);

            _textNodeFactory.SetPosition(Vector2.zero);
            _textNodeFactory.Displace(displacement);
        }

        private void RenderRecipeMapRecursive(IngredientTree parent)
        {
            Vector2 parentPosition = new Vector2(parent.xPosition
                , parent.yPosition * verticalSpacing);

            _ingredientNodeFactory.SpawnIngredientNode
                (parent.ingredientData, parentPosition);

            // Horizontal Lines            
            if (parent.childrenTreeList.Count > 1)
            {
                ApplianceData thisAppliance = _allFoodData.recipeResultDict
                       [parent.ingredientData].applianceData;

                Vector2 leftPosition = new Vector2(
                    parent.GetLeftMostChild().xPosition
                    , parent.GetLeftMostChild().yPosition
                    * verticalSpacing);

                Vector2 rightPosition = new Vector2(
                        parent.GetRightMostChild().xPosition
                        , parent.GetRightMostChild().yPosition
                        * verticalSpacing);

                _edgeLineFactory.SpawnEdgeLine(rightPosition
                    , leftPosition , verticalSpacing
                    , 0, thisAppliance, false);
            }

            foreach (IngredientTree child in parent.childrenTreeList)
            {
                RenderRecipeMapRecursive(child);

                // Edge line to each child
                //Vector2 childPosition = new Vector2(child.xPosition
                //    , -child.yPosition * verticalSpacing);

                // Vertical Lines
                Vector2 childPosition = new Vector2(
                    parent.GetMiddleChildXPosition()
                    , child.yPosition * verticalSpacing);

                //ApplianceData thisAppliance = _allFoodData.recipeResultDict
                //      [parent.ingredientData].applianceData;

                ApplianceData thisAppliance = child.recipeDataEdge.applianceData;

                _edgeLineFactory.SpawnEdgeLine(childPosition,
                    parentPosition, verticalSpacing
                    , verticalLineGap, thisAppliance, true);
            }
        }

        private void RenderUtilityMapRecursive(IngredientTree parent)
        {
            Vector2 parentPosition = new Vector2(parent.xPosition
                , -parent.yPosition * verticalSpacing);

            _ingredientNodeFactory.SpawnIngredientNode
                (parent.ingredientData, parentPosition);

            foreach (IngredientTree child in parent.childrenTreeList)
            {
                RenderUtilityMapRecursive(child);

                // Edge line to each child
                Vector2 childPosition = new Vector2(child.xPosition
                    , -child.yPosition * verticalSpacing);

                // Vertical Lines
                //Vector2 childPosition = new Vector2(
                //    parent.GetMiddleChildXPosition()
                //    , child.yPosition * verticalSpacing);

                ApplianceData thisAppliance = child.recipeDataEdge.applianceData;

                _edgeLineFactory.SpawnEdgeLine(parentPosition
                    , childPosition, verticalSpacing
                    , verticalLineGap, thisAppliance, true);
            }
        }
    }
}