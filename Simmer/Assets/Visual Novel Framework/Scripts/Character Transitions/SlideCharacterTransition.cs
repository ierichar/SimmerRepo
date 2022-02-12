using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Simmer.VN
{
    [CreateAssetMenu(fileName = "SlideCharacterTransition", menuName = "VN Framework/Character Transitions/SlideCharacterTransition")]
    public class SlideCharacterTransition : CharacterTransition
    {
        public Ease enterEase;
        public Ease exitEase;

        public override IEnumerator Co_EnterScreen(VN_Character character, MonoBehaviour caller)
        {
            Vector2 endPosition = VN_Util.GetTransitionTarget(
                character, CharacterData.TransitionDirection.enter);

            character.state = VN_Character.State.active;
            character.VN_CharSprite.color = character.manager.characterManager.nonSpeakingColor;

            yield return caller.StartCoroutine(Co_Move(character, endPosition, enterEase));
        }

        public override IEnumerator Co_ExitScreen(VN_Character character, MonoBehaviour caller)
        {
            Vector2 endPosition = VN_Util.GetTransitionTarget(
                character, CharacterData.TransitionDirection.exit);

            character.state = VN_Character.State.hidden;
            character.VN_CharSprite.color = character.manager.characterManager.nonSpeakingColor;

            yield return caller.StartCoroutine(Co_Move(character, endPosition, exitEase));
        }

        IEnumerator Co_Move(VN_Character character, Vector2 endPosition, Ease ease)
        {
            bool waitingForComplete = true;
            character.rectTransform.DOAnchorPos(endPosition, character.data.transitionDuration)
                .OnComplete(() => waitingForComplete = false)
                .SetEase(ease);

            yield return new WaitUntil(() => waitingForComplete == false);
        }

    }
}