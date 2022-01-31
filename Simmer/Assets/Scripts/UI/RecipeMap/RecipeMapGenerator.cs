
using UnityEngine;

using Simmer.FoodData;
using Simmer.Appliance;

namespace Simmer.UI.RecipeMap
{
    public class RecipeMapGenerator : MonoBehaviour
    {
        private RecipeMapManager _recipeMapManager;
        private IngredientNodeFactory _ingredientNodeFactory;
        private TreeNodePositioning _treeNodePositioning;
        private AllFoodData _allFoodData;

        [SerializeField] ImageManager linePrefab;
        [SerializeField] private IngredientData _apexIngredient;
        [SerializeField] private float verticalSpacing;

        public void Construct(RecipeMapManager recipeMapManager)
        {
            _recipeMapManager = recipeMapManager;
            _ingredientNodeFactory = recipeMapManager.ingredientNodeFactory;
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

                ImageManager thisLine = RenderLine(thisPosition, childPosition);
                Color thisColor = _allFoodData.recipeResultDict
                    [tree.ingredientData].applianceData.colorCode;
                thisLine.SetColor(thisColor);
            }
        }

        private ImageManager RenderLine(Vector2 v1, Vector2 v2)
        {
            ImageManager thisLine = Instantiate(linePrefab, transform);
            thisLine.Construct();
            Vector2 dist = v1 - v2;
            Vector2 halfDist = dist / 2;
            thisLine.rectTransform.anchoredPosition
                = v1 + halfDist + new Vector2(0, -verticalSpacing);
            thisLine.rectTransform.rotation
                = Quaternion.Euler(new Vector3(0, 0
                , Mathf.Atan2(dist.x, dist.y) * Mathf.Rad2Deg));

            thisLine.rectTransform.sizeDelta = new Vector2(
                10, Vector2.Distance(v1, v2));

            return thisLine;
        }

    }
}