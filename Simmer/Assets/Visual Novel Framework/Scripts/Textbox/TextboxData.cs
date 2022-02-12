using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.VN
{
    [CreateAssetMenu(fileName = "New TextboxData", menuName = "VN Framework/Data/TextboxData")]
    public class TextboxData : ScriptableObject
    {
        [Header("Art Assets")]
        public Sprite textboxSprite;
        public Sprite nameboxSprite;
        public Sprite settingsSprite;
        public Sprite skipSprite;

        [Tooltip("Text box Y offset upwards from the bottom of the screen when active")]
        public float activeOffset;
        [Tooltip("Text box Y offset downwards from the bottom of the screen when hidden")]
        public float hiddenOffset;

        [Header("UI Settings")]
        [Tooltip("Duration of text box enter/exit transition in seconds")]
        public float textboxTransitionDuration = 1;
        [Tooltip("TextboxTransition scriptable object doing the transition movement")]
        public TextboxTransition textboxTransition;

        [System.Serializable]
        public struct CornerDecor
        {
            public Sprite sprite;
            public Vector2 positionOffset;
            public Quaternion rotationOffset;
        }
        [Tooltip("List of different corner dectorations")]
        public List<CornerDecor> cornerDecorList;

        public Sprite FindCornerDecorSprite(string target)
        {
            CornerDecor decor = cornerDecorList.Find(x => x.sprite.name == target);
            return decor.sprite;
        }

        public Sprite FindCornerDecorSprite(Sprite sprite)
        {
            CornerDecor decor = cornerDecorList.Find(x => x.sprite == sprite);
            return decor.sprite;
        }

        public (Vector2, Quaternion) GetCornerDecorOffsets(Sprite sprite)
        {
            CornerDecor decor = cornerDecorList.Find(x => x.sprite == sprite);
            return (decor.positionOffset, decor.rotationOffset);
        }
    }
}