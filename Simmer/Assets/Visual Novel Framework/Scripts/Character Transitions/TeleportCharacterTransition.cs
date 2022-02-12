using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.VN
{
    [CreateAssetMenu(fileName = "TeleportCharacterTransition", menuName = "VN Framework/Character Transitions/TeleportCharacterTransition")]
    public class TeleportCharacterTransition : CharacterTransition
    {
        public override IEnumerator Co_ExitScreen(VN_Character character, MonoBehaviour caller)
        {
            Vector2 endPosition = VN_Util.GetTransitionTarget(
                character, CharacterData.TransitionDirection.exit);

            yield return caller.StartCoroutine(Co_TeleportTransition(character, endPosition));
        }

        public override IEnumerator Co_EnterScreen(VN_Character character, MonoBehaviour caller)
        {
            Vector2 endPosition = VN_Util.GetTransitionTarget(
                character, CharacterData.TransitionDirection.enter);

            yield return caller.StartCoroutine(Co_TeleportTransition(character, endPosition));
        }

        IEnumerator Co_TeleportTransition(VN_Character character, Vector2 endPosition)
        {
            character.rectTransform.anchoredPosition = endPosition;
            yield break;
        }
    }
}