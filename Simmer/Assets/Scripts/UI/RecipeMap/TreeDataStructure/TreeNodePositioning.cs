using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Simmer.FoodData;
using Simmer.Appliance;

namespace Simmer.UI.RecipeMap
{
    public class TreeNodePositioning : MonoBehaviour
    {
        private RecipeMapManager _recipeMapManager;
        private Dictionary<IngredientData, RecipeData> recipeResultDict
            = new Dictionary<IngredientData, RecipeData>();

        [SerializeField] private float siblingDistance;
        [SerializeField] private float treeDistance;

        [SerializeField] private float nodeSize;

        [SerializeField] private SpecialNodeData _genericShop;

        public void Construct(RecipeMapManager recipeMapManager)
        {
            _recipeMapManager = recipeMapManager;

            recipeResultDict = _recipeMapManager.allFoodData.recipeResultDict;
        }

        public IngredientTree SpawnRecipeMap(IngredientData apexIngredient)
        {
            IngredientTree apexTree =
                new IngredientTree(apexIngredient, null, null, null);

            ConstructRecipeMapTree(apexTree);
            PositionNodes(apexTree);

            return apexTree;
        }

        public IngredientTree SpawnUtilityMap(IngredientData apexIngredient)
        {
            IngredientTree apexTree =
                new IngredientTree(apexIngredient, null, null, null);

            ConstructUtilityMapTree(apexTree);
            PositionNodes(apexTree);

            return apexTree;
        }

        private void PositionNodes(IngredientTree apexTree)
        {
            InitializeNodes(apexTree, 0);

            // assign initial X and Mod values for nodes
            CalculateInitialX(apexTree);

            // ensure no node is being drawn off screen
            CheckAllChildrenOnScreen(apexTree);

            // assign final X values to nodes
            CalculateFinalPositions(apexTree, 0);
        }

        private void ConstructRecipeMapTree(IngredientTree parent)
        {
            if (recipeResultDict.ContainsKey(parent.ingredientData))
            {
                RecipeData thisRecipe = recipeResultDict[parent.ingredientData];
                foreach (IngredientData ingredient
                    in thisRecipe.ingredientDataList)
                {
                    IngredientTree newChild
                        = new IngredientTree(ingredient, parent, thisRecipe, null);
                    parent.childrenTreeList.Add(newChild);

                    ConstructRecipeMapTree(newChild);
                }
            }
        }

        private void ConstructUtilityMapTree(IngredientTree parent)
        {
            Dictionary<ApplianceData, List<RecipeData>>
                applianceRecipeListDict = parent.ingredientData
                    .applianceRecipeListDict;

            if (applianceRecipeListDict.Count == 0)
            {
                //// Add sell node to all leaves
                //IngredientTree newChild = new IngredientTree
                //        (null, parent, null, _genericShop);
                //parent.childrenTreeList.Add(newChild);

                return;
            }

            foreach (var pair in applianceRecipeListDict)
            {
                foreach(RecipeData recipeData in pair.Value)
                {
                    IngredientTree newChild = new IngredientTree
                        (recipeData.resultIngredient, parent, recipeData, null);
                    parent.childrenTreeList.Add(newChild);

                    ConstructUtilityMapTree(newChild);
                }
            }
        }

        private void InitializeNodes(IngredientTree node, int depth)
        {
            node.xPosition = -1;
            node.yPosition = depth;
            node.modifer = 0;

            foreach (IngredientTree child in node.childrenTreeList)
                InitializeNodes(child, depth + 1);
        }

        private void CalculateInitialX(IngredientTree tree)
        {
            foreach (var child in tree.childrenTreeList)
                CalculateInitialX(child);

            // if no children
            if (tree.IsLeaf())
            {
                // if there is a previous sibling in this set, set X to prevous sibling + designated distance
                if (!tree.IsLeftMost())
                    tree.xPosition = tree.GetPreviousSibling().xPosition
                        + nodeSize + siblingDistance;
                else
                    // if this is the first node in a set, set X to 0
                    tree.xPosition = 0;
            }
            // if there is only one child
            else if (tree.childrenTreeList.Count == 1)
            {
                // if this is the first node in a set, set it's X value equal to it's child's X value
                if (tree.IsLeftMost())
                {
                    tree.xPosition = tree.childrenTreeList[0].xPosition;
                }
                else
                {
                    tree.xPosition = tree.GetPreviousSibling().xPosition
                        + nodeSize + siblingDistance;
                    tree.modifer = tree.xPosition
                        - tree.childrenTreeList[0].xPosition;
                }
            }
            else
            {
                var leftChild = tree.GetLeftMostChild();
                var rightChild = tree.GetRightMostChild();
                var mid = (leftChild.xPosition + rightChild.xPosition) / 2;

                if (tree.IsLeftMost())
                {
                    tree.xPosition = mid;
                }
                else
                {
                    tree.xPosition = tree.GetPreviousSibling().xPosition
                        + nodeSize + siblingDistance;
                    tree.modifer = tree.xPosition - mid;
                }
            }

            if (tree.childrenTreeList.Count > 0 && !tree.IsLeftMost())
            {
                // Since subtrees can overlap, check for conflicts and shift tree right if needed
                CheckForConflicts(tree);
            }

        }

        private void CheckAllChildrenOnScreen(IngredientTree tree)
        {
            var nodeContour = new Dictionary<int, float>();
            GetLeftContour(tree, 0, ref nodeContour);

            float shiftAmount = 0;
            foreach (var y in nodeContour.Keys)
            {
                if (nodeContour[y] + shiftAmount < 0)
                    shiftAmount = (nodeContour[y] * -1);
            }

            if (shiftAmount > 0)
            {
                tree.xPosition += shiftAmount;
                tree.modifer += shiftAmount;
            }
        }

        private static void CalculateFinalPositions(IngredientTree tree, float modSum)
        {
            tree.xPosition += modSum;
            modSum += tree.modifer;

            foreach (var child in tree.childrenTreeList)
                CalculateFinalPositions(child, modSum);

            if (tree.childrenTreeList.Count == 0)
            {
                tree.width = tree.xPosition;
                tree.height = tree.yPosition;
            }
            else
            {
                tree.width = tree.childrenTreeList
                    .OrderByDescending(p => p.width).First().width;
                tree.height = tree.childrenTreeList
                    .OrderByDescending(p => p.height).First().height;
            }
        }

        private void CheckForConflicts(IngredientTree tree)
        {
            var minDistance = treeDistance + nodeSize;
            var shiftValue = 0F;

            var nodeContour = new Dictionary<int, float>();
            GetLeftContour(tree, 0, ref nodeContour);

            var sibling = tree.GetLeftMostSibling();
            while (sibling != null && sibling != tree)
            {
                var siblingContour = new Dictionary<int, float>();
                GetRightContour(sibling, 0, ref siblingContour);

                for (int level = tree.yPosition + 1;
                    level <= Mathf.Min(siblingContour.Keys.Max()
                    , nodeContour.Keys.Max()); level++)
                {
                    var distance = nodeContour[level] - siblingContour[level];
                    if (distance + shiftValue < minDistance)
                    {
                        shiftValue = minDistance - distance;
                    }
                }

                if (shiftValue > 0)
                {
                    tree.xPosition += shiftValue;
                    tree.modifer += shiftValue;

                    CenterNodesBetween(tree, sibling);

                    shiftValue = 0;
                }

                sibling = sibling.GetNextSibling();
            }
        }

        private void CenterNodesBetween(IngredientTree leftTree
            , IngredientTree rightTree)
        {
            var leftIndex = leftTree.parentTree.childrenTreeList.IndexOf(rightTree);
            var rightIndex = leftTree.parentTree.childrenTreeList.IndexOf(leftTree);

            var numNodesBetween = (rightIndex - leftIndex) - 1;

            if (numNodesBetween > 0)
            {
                var distanceBetweenNodes = (leftTree.xPosition
                    - rightTree.xPosition) / (numNodesBetween + 1);

                int count = 1;
                for (int i = leftIndex + 1; i < rightIndex; i++)
                {
                    var middleNode = leftTree.parentTree.childrenTreeList[i];

                    var desiredX = rightTree.xPosition + (distanceBetweenNodes * count);
                    var offset = desiredX - middleNode.xPosition;
                    middleNode.xPosition += offset;
                    middleNode.modifer += offset;

                    count++;
                }

                CheckForConflicts(leftTree);
            }
        }

        private void GetLeftContour(IngredientTree tree, float modSum, ref Dictionary<int, float> values)
        {
            if (!values.ContainsKey(tree.yPosition))
                values.Add(tree.yPosition, tree.xPosition + modSum);
            else
                values[tree.yPosition] = Mathf.Min(
                    values[tree.yPosition], tree.xPosition + modSum);

            modSum += tree.modifer;
            foreach (var child in tree.childrenTreeList)
            {
                GetLeftContour(child, modSum, ref values);
            }
        }

        private void GetRightContour(IngredientTree tree, float modSum, ref Dictionary<int, float> values)
        {
            if (!values.ContainsKey(tree.yPosition))
                values.Add(tree.yPosition, tree.xPosition + modSum);
            else
                values[tree.yPosition] = Mathf.Max(
                    values[tree.yPosition], tree.xPosition + modSum);

            modSum += tree.modifer;
            foreach (var child in tree.childrenTreeList)
            {
                GetRightContour(child, modSum, ref values);
            }
        }

        public float GetLowestDepth(IngredientTree tree, float currentMin)
        {
            if (tree.IsLeaf())
            {
                return Mathf.Max(tree.yPosition, currentMin);
            }

            List<float> lowestYList = new List<float>();

            foreach (var child in tree.childrenTreeList)
            {
                lowestYList.Add(GetLowestDepth(child, currentMin));
            }

            return Mathf.Max(lowestYList.ToArray());
        }
    }
}