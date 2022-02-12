using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Simmer.VN
{
	public class VN_CharacterManager : MonoBehaviour
	{
		[Tooltip("CharacterData of the player in the VN")]
		[SerializeField] private CharacterData PlayerCharacterData;
		[Tooltip("Generic VN_Character GameObjects; There should be only 2 in a scene")]
		public List<VN_Character> CharacterObjects;
		[Tooltip("List of needed character data to pull from")]
		public List<CharacterData> AllCharacterData;

		[Tooltip("Color of characters when speaking")]
		public Color speakingColor;
		[Tooltip("Color of characters when not speaking")]
		public Color nonSpeakingColor;
		[Tooltip("How long it takes to transition to speaking/nonspeaking color")]
		public float speakerLightDuration;
		[Tooltip("Ease of transition to speaking/nonspeaking color")]
		public Ease speakerLightEase;

		private VN_Manager manager;

		public void Construct(VN_Manager manager)
		{
			this.manager = manager;
		}

		public IEnumerator UpdateSpeakerLight(VN_Character speaker)
		{
			foreach (VN_Character character in CharacterObjects)
			{
				if (character.state == VN_Character.State.active && character.data != null)
				{
					if (character == speaker)
					{
						StartCoroutine(character.TweenColor(character.VN_CharSprite,
							speakingColor, speakerLightDuration, speakerLightEase));
					}
					else
					{
						StartCoroutine(character.TweenColor(character.VN_CharSprite,
							nonSpeakingColor, speakerLightDuration, speakerLightEase));
					}
				}
			}

			yield return null;
		}

		public IEnumerator ResetCharacters()
		{
			foreach (VN_Character charObj in CharacterObjects)
			{
				if (charObj.data != null)
				{
					// Do same thing as CharExit
					yield return StartCoroutine(charObj.data.transition
						.Co_ExitScreen(charObj, this));
					charObj.ChangeSprite("");
					charObj.SetData(null);
				}
			}
		}
	}
}