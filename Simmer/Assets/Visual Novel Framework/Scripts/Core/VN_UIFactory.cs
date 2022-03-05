using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

using TMPro;

using DG.Tweening;

namespace Simmer.VN
{
	public class VN_UIFactory : MonoBehaviour
	{
		// UI Prefabs
		[SerializeField]
		[Tooltip("Used for VN name text")]
		private TextMeshProUGUI nameTextPrefab = null;
		[SerializeField]
		[Tooltip("Used for VN content text")]
		private TextMeshProUGUI contentTextPrefab = null;
		[SerializeField]
		[Tooltip("Used for VN choice buttons")]
		private Button buttonPrefab = null;

		private VN_Manager manager;
		private VN_AudioManager audioManager;

		public void Construct(VN_Manager VN_Manager, VN_AudioManager audioManager)
		{
			manager = VN_Manager;
			this.audioManager = audioManager;
		}

		/**
		 * Creates a Text object of the content of a story
		 * 
		 * @param text: the content of the story
		 * @return: the Text object of the content
		 */
		public TextMeshProUGUI CreateContentView(string text)
		{
			TextMeshProUGUI contentText = Instantiate(contentTextPrefab);
			contentText.text = text;
			contentText.transform.SetParent(manager.TextCanvas.transform, false);
			return contentText;
		}

		/**
		 * Creates a Text object of the name of a character
		 * 
		 * @param name: the name of the character
		 * @return: the Text object of the the character's name
		 */
		public TextMeshProUGUI CreateNameTextView(string name)
		{
			TextMeshProUGUI nameText = Instantiate(nameTextPrefab);
			nameText.transform.SetParent(manager.NameCanvas.transform, false);
			// Update NameText
			if (name == "Narrator")
			{
				nameText.text = "";
				manager.contentTextObj.fontStyle = FontStyles.Italic;
			}
			else
			{
				nameText.text = name;
			}

			return nameText;
		}

		/**
		 * Creates a buttons for a choice in a story
		 * 
		 * @param text: the text and choice for the button
		 * @return: the Button object for the choice
		 */
		public Button CreateChoiceView(string text)
		{
			// Creates the button from a prefab
			Button choice = Instantiate(buttonPrefab);
			choice.transform.SetParent(manager.ButtonCanvas.transform, false);

			CanvasGroup buttonCanvasGroup =
				choice.gameObject.GetComponent<CanvasGroup>();
			buttonCanvasGroup.interactable = false;

			choice.transform.localScale = Vector3.zero;
			choice.transform.DOScale(1, 0.5f).SetEase(Ease.OutSine)
				.OnComplete(() => { buttonCanvasGroup.interactable = true; });

			// Gets the text from the button prefab
			Text choiceText = choice.GetComponentInChildren<Text>();
			choiceText.text = text;

			return choice;
		}

		// Creates a button to restart the story and resets the story if the button is clicked
		public void CreateEndStoryButton()
		{
			Button choice = CreateChoiceView("End");
			choice.onClick.AddListener(delegate
			{
				//audioManager.PlayAudio(audioManager.buttonClick);
				manager.OnEndStory.Invoke();
				if (manager.transitionSceneOnEnd)
				{
					Debug.LogError(this + " Error:" +
						" transitionSceneOnEnd not implemented");
				}
				else
				{
					manager.ForceExitVN();
				}
			});
		}

		// Creates a button to start the story
		public void CreateStartStoryButton()
		{
			Button choice = CreateChoiceView("Start story");
			choice.onClick.AddListener(delegate
			{
				audioManager.PlayAudio(audioManager.buttonClick);
				manager.StartStory();
			});
		}

		// Creates all buttons correlating to a choice
		public void CreateAllChoiceButtons()
		{
			// Display all the choices, if there are any!
			if (manager.Story.currentChoices.Count > 0)
			{
				for (int i = 0; i < manager.Story.currentChoices.Count; i++)
				{
					Choice choice = manager.Story.currentChoices[i];
					Button button = CreateChoiceView(choice.text.Trim());
					// Tell the button what to do when we press it
					button.onClick.AddListener(delegate
					{
						audioManager.PlayAudio(audioManager.buttonClick);
						OnClickChoiceButton(choice);
					});
				}
			}
			// If there are no choices on this line of text
			// And add a button to continue
			else if (manager.Story.canContinue)
			{
				// Commented out because clicking anywhere now continues
				//Button button = CreateChoiceView("Continue");
				//button.onClick.AddListener(delegate
				//{
				//    manager.RefreshView();
				//});
			}
			// If there is no more content, prompt to restart
			else
			{
				CreateEndStoryButton();
			}
		}

		// Determines which choice the character selected and plays the corresponding text
		public void OnClickChoiceButton(Choice choice)
		{
			if (VN_Util.VN_Debug)
			{
				VN_Util.VNDebugPrint("Chose choice: \"" + choice.text + "\"", this);
			}
			manager.Story.ChooseChoiceIndex(choice.index);
			manager.RefreshView();
		}
	}
}