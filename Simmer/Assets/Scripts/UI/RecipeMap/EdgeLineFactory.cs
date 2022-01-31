using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI.RecipeMap
{
    public class EdgeLineFactory : MonoBehaviour
    {
        [SerializeField] EdgeLine edgeLinePrefab;
        private List<EdgeLine> edgeLineList
            = new List<EdgeLine>();

        public void Construct()
        {

        }

        public EdgeLine SpawnEdgeLine(Vector2 v1
            , Vector2 v2
            , float verticalSpacing
            , Color thisColor)
        {
            EdgeLine thisLine = Instantiate(edgeLinePrefab, transform);
            thisLine.Construct(v1
                , v2
                , verticalSpacing
                , thisColor);

            edgeLineList.Add(thisLine);

            return thisLine;
        }

        public void ClearAllEdgeLines()
        {
            for (int i = 0; i < edgeLineList.Count; ++i)
            {
                Destroy(edgeLineList[i].gameObject);
            }
            edgeLineList.Clear();
        }
    }
}