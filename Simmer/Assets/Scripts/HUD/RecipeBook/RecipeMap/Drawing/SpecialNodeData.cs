using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI.RecipeMap
{
    [CreateAssetMenu(fileName = "New SpecialNodeData"
    , menuName = "RecipeMap/SpecialNodeData")]

    public class SpecialNodeData : ScriptableObject
    {
        public Sprite sprite;

        public string tooltipHeader;
        public string tooltipBody;
    }
}