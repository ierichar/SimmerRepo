
using UnityEngine;

using Simmer.FoodData;
using Simmer.Items;

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

            _recipeMapManager.apexRecipeSlot.onItemDrop
                .AddListener(RenderTreeFromDrop);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                RenderTree(_apexIngredient);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _ingredientNodeFactory.ClearAll();
                _edgeLineFactory.ClearAll();
            }

        }

        private void RenderTreeFromDrop(ItemBehaviour itemBehaviour)
        {
            RenderTree(itemBehaviour.foodItem.ingredientData);
        }

        public void RenderTree(IngredientData apexIngredient)
        {
            _ingredientNodeFactory.ClearAll();
            _edgeLineFactory.ClearAll();

            IngredientTree apexTree =
                    _treeNodePositioning.SpawnTree(apexIngredient);

            RenderTreeRecursive(apexTree);

            // Center on apexPositionMarker
            Vector2 oldApexPosition
                = new Vector2(apexTree.xPosition, apexTree.yPosition);
            Vector2 displacement = apexPositionMarker
                .anchoredPosition - oldApexPosition;

            // Center tree vertical midpoint on apexPositionMarker
            float maxDepth = _treeNodePositioning
                .GetLowestDepth(apexTree, 0);
            float yDisplacement = (apexTree.yPosition - maxDepth)/2;
            displacement += new Vector2(0, yDisplacement * verticalSpacing);

            _ingredientNodeFactory.SetPosition(Vector2.zero);
            _ingredientNodeFactory.Displace(displacement);

            _edgeLineFactory.SetPosition(Vector2.zero);
            _edgeLineFactory.Displace(displacement);

            _textNodeFactory.SetPosition(Vector2.zero);
            _textNodeFactory.Displace(displacement);
        }

        private void RenderTreeRecursive(IngredientTree parent)
        {
            Vector2 parentPosition = new Vector2(parent.xPosition
                , parent.yPosition * verticalSpacing);

            _ingredientNodeFactory.SpawnIngredientNode
                (parent.ingredientData, parentPosition);

            // Horizontal Lines
            if (parent.childrenTreeList.Count > 1)
            {
                Color thisColor = _allFoodData.recipeResultDict
                    [parent.ingredientData].applianceData.colorCode;

                Vector2 leftPosition = new Vector2(
                    parent.GetLeftMostChild().xPosition
                    , parent.GetLeftMostChild().yPosition
                    * verticalSpacing);

                Vector2 rightPosition = new Vector2(
                        parent.GetRightMostChild().xPosition
                        , parent.GetRightMostChild().yPosition
                        * verticalSpacing);

                _edgeLineFactory.SpawnEdgeLine(leftPosition
                    , rightPosition, verticalSpacing
                    , 0, thisColor);
            }

            foreach (IngredientTree child in parent.childrenTreeList)
            {
                RenderTreeRecursive(child);

                // Edge line to each child
                //Vector2 childPosition = new Vector2(child.xPosition
                //    , -child.yPosition * verticalSpacing);

                // Vertical Lines
                Vector2 childPosition = new Vector2(
                    parent.GetMiddleChildXPosition()
                    , child.yPosition * verticalSpacing);

                Color thisColor = _allFoodData.recipeResultDict
                    [parent.ingredientData].applianceData.colorCode;

                _edgeLineFactory.SpawnEdgeLine(parentPosition
                    , childPosition, verticalSpacing
                    , verticalLineGap, thisColor);

                //IngredientTree nextSibling = child.GetNextSibling();

                //if(nextSibling != null)
                //{
                //    float middleX = (nextSibling.xPosition
                //        - child.xPosition) / 2;
                //    Vector2 textPosition = new Vector2 (middleX
                //        , -child.yPosition * verticalSpacing);

                //    _textNodeFactory.SpawnNode("+", textPosition);
                //}
            }
        }

    }
}