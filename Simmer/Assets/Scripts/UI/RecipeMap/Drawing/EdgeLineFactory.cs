using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI.RecipeMap
{
    public class EdgeLineFactory : MonoBehaviour
    {
        private RectTransform _rectTransform;

        [SerializeField] EdgeLine edgeLinePrefab;
        private List<EdgeLine> edgeLineList
            = new List<EdgeLine>();

        public void Construct()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetPosition(Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
        }

        public void Displace(Vector2 displacement)
        {
            _rectTransform.anchoredPosition += displacement;
        }

        public EdgeLine SpawnEdgeLine(Vector2 v1
            , Vector2 v2
            , float verticalSpacing
            , float verticalLineGap
            , Color thisColor)
        {
            EdgeLine thisLine = Instantiate(edgeLinePrefab, transform);
            thisLine.Construct(v1
                , v2
                , verticalSpacing
                , verticalLineGap
                , thisColor);

            edgeLineList.Add(thisLine);

            return thisLine;
        }

        public void ClearAll()
        {
            for (int i = 0; i < edgeLineList.Count; ++i)
            {
                Destroy(edgeLineList[i].gameObject);
            }
            edgeLineList.Clear();
        }
    }
}