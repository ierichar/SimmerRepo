using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.VN
{
    [CreateAssetMenu(fileName = "New Character", menuName = "VN Framework/Data/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public enum ScenePosition { left, right };
        public enum MoveTransition { teleport, slide };
        public enum TransitionDirection { enter, exit };

        [Header("Settings")]
        [Tooltip("Position of character on the screen")]
        public ScenePosition scenePosition;
        [Tooltip("Distance in pixels away from the edge of the screen")]
        public int screenEdgeDistance;
        [Tooltip("Transition animation script for the character")]
        public CharacterTransition transition;
        [Tooltip("Duration in seconds of Character enter/exit transition")]
        public float transitionDuration;
        public float characterSpriteScale = 1;
        public float characterBoxScale = 1;

        [Header("Character's Art Assets")]
        [Tooltip("Starting sprite; make sure it exists in the sprites list")]
        public Sprite defaultSprite;
        public Sprite characterBox;
        [Tooltip("List of character's sprites")]
        public List<Sprite> characterSprites;
    }
}