using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.FoodData;

namespace Simmer.UI.RecipeMap
{
    public class IngredientTree
    {
        public float xPosition { get; set; }
        public int yPosition { get; set; }
        public float modifer { get; set; }
        public IngredientTree parentTree { get; set; }
        public List<IngredientTree> childrenTreeList { get; set; }

        public float width { get; set; }
        public int height { get; set; }

        public IngredientData ingredientData { get; set; }
        public RecipeData recipeDataEdge { get; set; }
        public SpecialNodeData specialNodeData { get; set; }

        public IngredientTree(IngredientData item
            , IngredientTree parent
            , RecipeData recipeDataEdge
            , SpecialNodeData specialNodeData)
        {
            ingredientData = item;
            parentTree = parent;
            this.recipeDataEdge = recipeDataEdge;
            this.specialNodeData = specialNodeData;
            childrenTreeList = new List<IngredientTree>();
        }

        public bool IsLeaf()
        {
            return childrenTreeList.Count == 0;
        }

        public bool IsLeftMost()
        {
            if (parentTree == null) return true;

            return parentTree.childrenTreeList[0] == this;
        }

        public bool IsRightMost()
        {
            if (parentTree == null) return true;

            return parentTree.childrenTreeList
                [parentTree.childrenTreeList.Count - 1] == this;
        }

        public IngredientTree GetPreviousSibling()
        {
            if (parentTree == null || IsLeftMost()) return null;

            return parentTree.childrenTreeList[
                parentTree.childrenTreeList.IndexOf(this) - 1];
        }

        public IngredientTree GetNextSibling()
        {
            if (parentTree == null || IsRightMost()) return null;

            return parentTree.childrenTreeList[
                parentTree.childrenTreeList.IndexOf(this) + 1];
        }

        public IngredientTree GetLeftMostSibling()
        {
            if (parentTree == null) return null;

            if (IsLeftMost()) return this;

            return parentTree.childrenTreeList[0];
        }

        public IngredientTree GetLeftMostChild()
        {
            if (childrenTreeList.Count == 0) return null;

            return childrenTreeList[0];
        }

        public IngredientTree GetRightMostChild()
        {
            if (childrenTreeList.Count == 0) return null;

            return childrenTreeList[childrenTreeList.Count - 1];
        }

        public float GetMiddleChildXPosition()
        {
            if (childrenTreeList.Count <= 2) return xPosition;

            float midpoint = (GetRightMostChild().xPosition
                - GetLeftMostChild().xPosition) / 2;
            return midpoint;
        }

        public override string ToString()
        {
            return ingredientData.name;
        }
    }
}