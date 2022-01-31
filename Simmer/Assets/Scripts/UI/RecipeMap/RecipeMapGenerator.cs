
using UnityEngine;

using Simmer.FoodData;
using Simmer.Appliance;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapGenerator : MonoBehaviour
    {
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
                IngredientTree apexTree =
                _treeNodePositioning.SpawnTree(_apexIngredient);

                RenderTree(apexTree);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _ingredientNodeFactory.ClearAllNodes();
                _edgeLineFactory.ClearAllEdgeLines();
            }

        }

        private void RenderTree(IngredientTree tree)
        {
            Vector2 thisPosition = new Vector2(tree.xPosition
                , -tree.yPosition * verticalSpacing);
            _ingredientNodeFactory.SpawnIngredientNode
                (tree.ingredientData, thisPosition);

            foreach (IngredientTree child in tree.childrenTreeList)
            {
                RenderTree(child);

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