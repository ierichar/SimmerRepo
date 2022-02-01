
using UnityEngine;

using Simmer.FoodData;
using Simmer.Appliance;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapGenerator : MonoBehaviour
    {
        [SerializeField] private RectTransform apexPositionMarker;
        private RecipeMapManager _recipeMapManager;
        private IngredientNodeFactory _ingredientNodeFactory;
        private EdgeLineFactory _edgeLineFactory;
        private TreeNodePositioning _treeNodePositioning;
        private AllFoodData _allFoodData;

        [SerializeField] private IngredientData _apexIngredient;
        [SerializeField] private float verticalSpacing;

        public void Construct(RecipeMapManager recipeMapManager)
        {
            _recipeMapManager = recipeMapManager;
            _ingredientNodeFactory = recipeMapManager.ingredientNodeFactory;
            _edgeLineFactory = recipeMapManager.edgeLineFactory;
            _treeNodePositioning = recipeMapManager.treeNodePositioning;
            _allFoodData = recipeMapManager.allFoodData;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                RenderTree();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _ingredientNodeFactory.ClearAll();
                _edgeLineFactory.ClearAll();
            }

        }

        public void RenderTree()
        {
            _ingredientNodeFactory.ClearAll();
            _edgeLineFactory.ClearAll();

            IngredientTree apexTree =
                    _treeNodePositioning.SpawnTree(_apexIngredient);

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
            displacement += new Vector2(0, -yDisplacement * verticalSpacing);

            _ingredientNodeFactory.SetPosition(Vector2.zero);
            _edgeLineFactory.SetPosition(Vector2.zero);
            _ingredientNodeFactory.Displace(displacement);
            _edgeLineFactory.Displace(displacement);
        }

        private void RenderTreeRecursive(IngredientTree tree)
        {
            Vector2 thisPosition = new Vector2(tree.xPosition
                , -tree.yPosition * verticalSpacing);
            _ingredientNodeFactory.SpawnIngredientNode
                (tree.ingredientData, thisPosition);

            foreach (IngredientTree child in tree.childrenTreeList)
            {
                RenderTreeRecursive(child);

                Vector2 childPosition = new Vector2(child.xPosition
                    , -child.yPosition * verticalSpacing);

                Color thisColor = _allFoodData.recipeResultDict
                    [tree.ingredientData].applianceData.colorCode;
                _edgeLineFactory.SpawnEdgeLine(thisPosition
                    , childPosition, verticalSpacing, thisColor);
            }
        }

    }
}