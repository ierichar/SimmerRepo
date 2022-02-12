using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.VN
{
    public class VN_Util
    {
        public static VN_Manager manager;
        public static bool VN_Debug;
        public static double startUpTime;

        public static string storedDebugString;

        public VN_Util(VN_Manager manager, bool debug)
        {
            VN_Util.manager = manager;
            VN_Debug = debug;
        }

        public static char[] toTrim = { '\"', ' ', '\n' };

        public static void VNDebugPrint(string message, Object context)
        {
            double timeSince = Time.realtimeSinceStartup - startUpTime;
            Debug.Log("Time: " + timeSince.ToString("F2") + " " + message, context);
        }

        /// <summary>
        /// Gets the Vector2 target position depending on the character's ScenePosition and
        /// given direction of transition for a VN_Character from a TransitionDirection
        /// </summary>
        /// <param name="character">VN_Character who needs the target position</param>
        /// <param name="direction">TransitionDirection of character's planned TransitionDirection</param>
        /// <returns>Vector2 target position after transition</returns>
        public static Vector2 GetTransitionTarget(VN_Character character,
            CharacterData.TransitionDirection direction)
        {
            CharacterData data = character.data;
            if (!data)
            {
                Debug.LogError("Cannot call GetTransitionTarget on a VN_Character with null CharacterData");
                return Vector2.zero;
            }
            float targetY = character.rectTransform.anchoredPosition.y;
            float targetX = 0;
            switch (direction)
            {
                case CharacterData.TransitionDirection.enter:
                    targetX = data.screenEdgeDistance;
                    switch (data.scenePosition)
                    {
                        // If on left, go right to be on target
                        case CharacterData.ScenePosition.left:
                            return new Vector2(targetX, targetY);
                        case CharacterData.ScenePosition.right:
                            // If on right, go left to be on target
                            return new Vector2(-targetX, targetY);
                    }
                    break;
                case CharacterData.TransitionDirection.exit:
                    targetX = data.screenEdgeDistance + character.rectTransform.sizeDelta.x;
                    switch (data.scenePosition)
                    {
                        // If on left, go left to be on target
                        case CharacterData.ScenePosition.left:
                            return new Vector2(-targetX, targetY);
                        case CharacterData.ScenePosition.right:
                            // If on right, go right to be on target
                            return new Vector2(targetX, targetY);
                    }
                    break;
            }

            // This should never happen
            Debug.LogError("Invalid transition target position found");
            return Vector2.zero;
        }

        /// <summary>
        /// Give a name string for a character, searches and returns the CharacterData
        /// of corresponding name if found in VN_Manager's AllCharacterData list.
        /// </summary>
        /// <param name="characterName">Case sensitive string of character whose CharacterData
        /// is to be searched for</param>
        /// <returns>If found, CharacterData of name characterName; otherwise null</returns>
        public static CharacterData FindCharacterData(string characterName)
        {
            // Get currentSpeaker by finding speakerName in CharacterObjects
            CharacterData character = manager.characterManager.AllCharacterData
                .Find(x => x.name == characterName);

            // Catch character being null
            if (!character)
            {
                character = null;
                Debug.LogError("Character of name " + characterName + " could not be found");
            }
            return character;
        }

        /// <summary>
        /// Finds the VN_Character that has CharacterData field equal to given
        /// CharacterData data in VN_Manager AllCharacterData list.
        /// </summary>
        /// <param name="data">CharacterData to look for in VN_Characters</param>
        /// <returns>If found, VN_Character that has data of param data; otherwise null</returns>
        public static VN_Character FindCharacterObj(CharacterData data)
        {
            CharacterData characterData = manager.characterManager.AllCharacterData
                .Find(x => x == data);

            if (!characterData)
            {
                Debug.LogError("Cannot find " + data.name + " in AllCharacterData");
                return null;
            }

            foreach (VN_Character charObj in manager.characterManager.CharacterObjects)
            {
                if (charObj.data == characterData) return charObj;
            }

            Debug.LogError("Cannot find data of " + data.name + " in CharacterObjects");
            return null;
        }

        /// <summary>
        /// Finds the VN_Character that has CharacterData field as null and
        /// has matching scenePosition as param data
        /// </summary>
        /// <param name="data">Character data to</param>
        /// <returns>If found, VN_Character that has data of param data; otherwise null</returns>
        public static VN_Character FindEmptyCharObj(CharacterData data)
        {
            CharacterData characterData = manager.characterManager.AllCharacterData.Find(x => x == data);

            if (!characterData)
            {
                Debug.LogError("Cannot find " + data.name + " in AllCharacterData");
                return null;
            }

            foreach (VN_Character charObj in manager.characterManager.CharacterObjects)
            {
                if (charObj.data == null &&
                    charObj.scenePosition.ToString() == data.scenePosition.ToString()) return charObj;
            }

            Debug.LogError("Cannot find available CharacterObject for " + data.name);
            return null;
        }

        /// <summary>
        /// Finds the TextboxData of string name target in VN_Manager AllTextboxData list.
        /// </summary>
        /// <param name="target">String name of TextboxData</param>
        /// <returns>If found, TextboxData that has name of target; otherwise null</returns>
        public static TextboxData FindTextboxData(string target)
        {
            TextboxData textboxData = manager.textboxManager.AllTextboxData.Find(x => x.name == target);
            if (!textboxData)
            {
                textboxData = null;
                Debug.LogError("TextboxData of name " + target + " could not be found");
            }
            return textboxData;
        }

        /// <summary>
        /// Finds the CornerDecor Sprite of name target in TextboxData data.
        /// </summary>
        /// <param name="data">TextboxData that is being searched</param>
        /// /// <param name="target">String name of the Sprite to look for in CornerDecor</param>
        /// <returns>If found, Sprite with name of target; otherwise null</returns>
        public static Sprite FindTextboxCornerDecor(TextboxData data, string target)
        {
            Sprite decor = data.FindCornerDecorSprite(target);
            if (!decor)
            {
                decor = null;
                Debug.LogError("Sprite of name " + target + " could not be found");
            }
            return decor;
        }

        /// <summary>
        /// Removes the first instance of string toRemove in string source
        /// </summary>
        /// <param name="source">String being modified</param>
        /// /// <param name="toRemove">String to remove from source</param>
        /// <returns>A new string result after removal</returns>
        public static string RemoveSubstring(string source, string toRemove)
        {
            return source.Remove(source.IndexOf(toRemove), toRemove.Length);
        }
    }
}