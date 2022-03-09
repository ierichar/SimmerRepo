using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

using Ink.Runtime;

namespace Simmer.VN
{
	/// <summary>
	/// Creates and manages VN components and flow
	/// Based off of BasicInkExample.cs in Plugins/Ink/Example/Scripts
	/// </summary>
	public class VN_Manager : MonoBehaviour
	{
		[Header("Transition Stuff (Unrelated to the things happening below)")]

		// General settings for the textbox and appearance of text
		[Header("Settings")]
		[Tooltip("ActiveState of VN. Set before play to make VN appear or be hidden on start")]
		public ActiveState activeState = ActiveState.hidden;
		public enum ActiveState { hidden, active }
		public bool beginOnStart;
		public bool transitionSceneOnEnd = true;
		public string nextScene;

		// The different speeds the text can go in characters per second
		[Header("Text Speeds")]
		[Tooltip("Normal speed of text in characters per second. Used in normal dialouge")]
		public float normalSpeed = 60;
		[Tooltip("Speed of the text (in characters per second) during a pause. Used at punctuation marks")]
		public float pauseSpeed = 10;

		// The Options Menu slider used to edit text speed during runtime
		[Header("Text Speed Slider")]
		[Tooltip("The Slider object itself")]
		public Slider textSpeedSlider;
		[Tooltip("Minumum speed for normal text")]
		public float minValue;
		[Tooltip("Maximum speed for normal text")]
		public float maxValue;

		// Dictionary of the different speeds (Dictionaries are not serializable).
		private Dictionary<string, float> TextSpeeds;
		// List of characters the the text will pause at
		// TODO Put all settings/data for this manager into a scriptable object
		public static readonly List<float> PauseChars = new List<float>() { ',', '.', '…', '!', '?', '\n', '-', '–' };

		// Objects needed to create story from JSON
		[Header("Required Objects")]
		[Tooltip("Intermediate file for Unity to work with Ink; Created when a .ink file is saved in Unity")]
		public TextAsset inkJSONAsset;
		public Story Story { get; private set; }

		[Header("Debugging")]
		public bool DebugEnabled;

		[Header("Public References")]
		public Canvas TextboxCanvas;
		public RectTransform textboxRectTransform;
		public Canvas TextCanvas;
		public Canvas ButtonCanvas;
		public Canvas NameCanvas;
		// Text object that displays text content
		public TextMeshProUGUI contentTextObj;
		// Text object that displays speaker name
		public TextMeshProUGUI nameTextObj;

		public Canvas Decor_RTCanvas;
		public Canvas Decor_LBCanvas;

		// Internal References & variables
		// Keep track of story creation event
		public static event Action<Story> OnCreateStory;
		// Invoked when StartStory is called
		public UnityEvent OnStartStory = new UnityEvent();
		// Invoked when EndStory button is pressed 
		public UnityEvent OnEndStory = new UnityEvent();
		// The content to be displayed
		private string currentLine = "";
		// The character who is currently speaking
		private VN_Character currentSpeaker = null;
		// The tags in effect on the current text
		private List<string> currentTags;

		// State of text displaying
		// Typing: content text is typing out char by char
		// Idle: text is done displaying and nothing is happening
		// Busy: VN is processing line, running commands
		// End: Ink Story has reached an end
		public enum VN_State { typing, idle, busy, end };
		[HideInInspector] public VN_State state;
		private UnityEvent OnTextTypeEnd = new UnityEvent();

		public enum MouseState { other, contentBox };
		[HideInInspector] public MouseState mouseState = MouseState.other;

		private IEnumerator currentTypingCoroutine;

		// Subordinate classes
		private VN_CommandCall CommandCall;
		[HideInInspector] public VN_UIFactory UIFactory;
		[HideInInspector] public VN_TextboxManager textboxManager;
		[HideInInspector] public VN_CharacterManager characterManager;
		[HideInInspector] public VN_AudioManager audioManager;
		[HideInInspector] public VN_SharedVariables sharedVariables;
		[HideInInspector] public VN_ScreenManager screenManager;

		// Get parse results from corountine
		private string speaker;
		private string content;

		//Functions involved directly in the frame-to-frame running of the sceen
		#region Unity gameloop

		// Called once upon initialization of the scene 
		// Adds the AddCharacter and SubtractCharacter functions to the AllCommands Dictionary
		public void Construct()
		{
			VN_Util Helper = new VN_Util(this, DebugEnabled);

			OnStartStory.AddListener(() =>
			{
				state = VN_State.idle;
			});

			OnTextTypeEnd.AddListener(() =>
			{
				state = VN_State.idle;
			});

			OnEndStory.AddListener(() =>
			{
				state = VN_State.end;
			});

			screenManager = GetComponent<VN_ScreenManager>();
			screenManager.Construct(this);

			sharedVariables = GetComponent<VN_SharedVariables>();
			sharedVariables.Construct(this);

			audioManager = GetComponent<VN_AudioManager>();
			audioManager.Construct(this);

			// Get CommandCall script in gameobject
			CommandCall = GetComponent<VN_CommandCall>();
			CommandCall.Construct(this);

			UIFactory = GetComponent<VN_UIFactory>();
			UIFactory.Construct(this, audioManager);
			textboxRectTransform = TextboxCanvas.GetComponent<RectTransform>();

			textboxManager = GetComponent<VN_TextboxManager>();
			textboxManager.Construct(this);

			characterManager = GetComponent<VN_CharacterManager>();
			characterManager.Construct(this);

			var VN_characters = GameObject.FindObjectsOfType<VN_Character>();

			foreach (VN_Character character in VN_characters)
			{
				character.Construct(this, characterManager);
			}

			// Initialize the TextSpeeds Dictionary
			TextSpeeds = new Dictionary<string, float>{
				{"Normal", normalSpeed},
				{"Pause", pauseSpeed}
			};

			// Setup for the slider
			if (textSpeedSlider)
			{
				textSpeedSlider.value = TextSpeeds["Normal"];
				textSpeedSlider.minValue = this.minValue;
				textSpeedSlider.maxValue = this.maxValue;
			}

			//Check to make sure all assigned text speeds are valid (i.e. speed > 0)
			foreach (float value in TextSpeeds.Values)
			{
				if (value <= 0) Debug.LogError("At least one speed value is not greater than 0. Please fix this in the inspector");
			}

			ClearContent();

			textboxManager.SetDefaultData();

			switch (activeState)
			{
				case ActiveState.hidden:
					float textboxHeight = textboxRectTransform.sizeDelta.y;
					float hiddenOffset = textboxManager.data.hiddenOffset;
					textboxRectTransform.anchoredPosition =
						new Vector2(0, -(textboxHeight + hiddenOffset));
					break;
				case ActiveState.active:
					float activeOffset = textboxManager.data.activeOffset;
					textboxRectTransform.anchoredPosition =
						new Vector2(0, activeOffset);
					break;
			}

			state = VN_State.end;

			if (beginOnStart) StartStory();
		}

		void Update()
		{
			// Skips the animation of text appearing if the spacebar or primary mouse button is pressed
			// TODO replace with input system to not hard code input bindings

			if (mouseState == MouseState.contentBox
				&& state == VN_State.idle)
			{
				if (Input.GetKeyDown(KeyCode.Space)
					|| Input.GetKeyDown(KeyCode.Mouse0))
				{
					//audioManager.PlayAudio(audioManager.buttonClick);
					RefreshView();
				}
			}

			if (mouseState == MouseState.contentBox
				&& state == VN_State.typing)
			{
				if (Input.GetKeyDown(KeyCode.Space)
					|| Input.GetKeyDown(KeyCode.Mouse0))
				{
					SkipSlowText();
				}
			}

			if (Input.GetKeyDown(KeyCode.F))
			{
				StopAllCoroutines();
				OnEndStory.Invoke();
			}
		}
		#endregion

		// Functions involved with managing the story on a high level
		#region Main Functions

		// Initializes a story based on the imported JSON file
		public void StartStory()
		{
			//var compiler = new Ink.Compiler(inkJSONAsset.text, new Ink.Compiler.Options
			//{
			//	countAllVisits = true,
			//	fileHandler = new UnityInkFileHandler(System.IO.Path.GetDirectoryName(inkJSONAsset.file))
			//});
			//Ink.Runtime.Story story = compiler.Compile();
			Story = new Story(inkJSONAsset.text);
			if (OnCreateStory != null) OnCreateStory(Story);
			RefreshView();

			if (VN_Util.VN_Debug)
			{
				VN_Util.startUpTime = Time.realtimeSinceStartup;
				VN_Util.VNDebugPrint("Start story: \"" + inkJSONAsset.name + "\"", this);
			}
			OnStartStory.Invoke();
		}

		// Remove all VN text & buttons, then starts displaying the text
		public void RefreshView()
		{
			if (Story.canContinue)
			{
				if (currentTypingCoroutine != null) StopCoroutine(currentTypingCoroutine);
				StopCoroutine(Co_DisplaySlowText());
				ClearContent();

				StartCoroutine(Co_DisplaySlowText());
			}
		}
		#endregion

		// Functions involved in the text appearing on the screen
		#region Slow Text Functions

		private IEnumerator Co_DisplaySlowText()
		{
			state = VN_State.busy;
			// Get the next line of text
			string nextLine = Story.Continue();

			// Get tags
			currentTags = Story.currentTags;

			// Parses the line for speakerName and sets currentLine to content
			yield return StartCoroutine(Co_ParseLine(nextLine));

			// Special case: Narrator is not a VN_Character and not in CharacterObjects
			if (speaker != "Narrator")
			{
				currentSpeaker = VN_Util.FindCharacterObj(
					VN_Util.FindCharacterData(speaker));
				// If valid speaker found, try changing emotion by tag
				if (currentSpeaker) tagChangeEmotion();
			}
			// Set currentLine to content
			currentLine = content;

			// If content is blank, skip making content
			if (string.IsNullOrEmpty(currentLine))
			{
				if (Story.canContinue)
				{
					StartCoroutine(Co_DisplaySlowText());
				}
				else
				{
					state = VN_State.end;
					UIFactory.CreateEndStoryButton();
				}
				yield break;
			}

			// Format text to have newline characters at porper locations
			currentLine = FormatText(currentLine);

			//// Instantiate story content
			contentTextObj = UIFactory.CreateContentView("");
			//// Instantiate character name text
			nameTextObj = UIFactory.CreateNameTextView(speaker);

			// Start displaying text content
			if (VN_Util.VN_Debug)
			{
				VN_Util.storedDebugString = currentLine;
				VN_Util.VNDebugPrint("Start display text: \"" + VN_Util.storedDebugString + "\"", this);
			}
			currentTypingCoroutine = Co_SlowText(contentTextObj);
			yield return StartCoroutine(currentTypingCoroutine);

			// Once done with showing text content, show all choice buttons
			UIFactory.CreateAllChoiceButtons();
		}

		// Displays the current text on screen, one char at a time, then creates the choice buttons when it's done
		private IEnumerator Co_SlowText(TextMeshProUGUI storyText)
		{
			state = VN_State.typing;
			float TextSpeed;

			yield return StartCoroutine(characterManager.UpdateSpeakerLight(currentSpeaker));

			foreach (char character in currentLine.ToCharArray())
			{
				storyText.text += character;

				if (PauseChars.Contains(character)) TextSpeed = TextSpeeds["Pause"];
				else TextSpeed = TextSpeeds["Normal"];

				// Delay appending more characters
				// 1/TextSpeed because WaitForSeconds(TextSpeed) is 1 char per x seconds
				// Convert to x char per second by inverting
				yield return new WaitForSeconds(1 / TextSpeed);
			}
			if (VN_Util.VN_Debug)
			{
				VN_Util.VNDebugPrint("End display text: \"" + VN_Util.storedDebugString + "\"", this);
			}
			OnTextTypeEnd.Invoke();
		}

		private string FormatText(string text)
		{
			string result = "";
			foreach (char character in text.ToCharArray())
			{
				if (character == '…')
				{
					result += "...";
				}
				else
				{
					result += character;
				}
			}

			return result;
		}

		// Makes the remaining text appear instantly, then creates the choice buttons
		private void SkipSlowText()
		{
			if (state == VN_State.typing)
			{
				StopCoroutine(currentTypingCoroutine);
				contentTextObj.text = currentLine;
				UIFactory.CreateAllChoiceButtons();

				if (VN_Util.VN_Debug)
				{
					VN_Util.VNDebugPrint("Skip slow text: \"" + VN_Util.storedDebugString + "\"", this);
				}
				OnTextTypeEnd.Invoke();
			}
		}
		#endregion

		// Methods to extract and/or edit data from lines of the story
		#region Line Reading

		// Changes the current emotion portrayed by the text
		void tagChangeEmotion()
		{
			// Check that there are any tags
			if (currentTags.Count > 0)
			{
				// Get first tag in currentTags
				string emotionTag = currentTags[0];
				currentSpeaker.ChangeSprite(emotionTag);

				if (VN_Util.VN_Debug)
				{
					VN_Util.VNDebugPrint("Tag change emotion: \"" + emotionTag + "\"", this);
				}
			}
		}

		/**
		* On a specified line (string of text), determines who the speaker is and what they're saying
		* 
		* @param line: The line being parsed
		* @return: tuple of 2 elements with element 0 being the speaker's name and 1 being what the speaker says
		*/
		IEnumerator Co_ParseLine(string line)
		{
			// If line is in format "[character name]: [text to be spoken]"
			// lineSplit holds [character name] in index 0 and [text to be spoken] in index 1
			string[] lineSplit = line.Split(':');

			if (lineSplit.Length == 2)
			{
				// Trim removes any white space from the beginning or end.
				speaker = lineSplit[0].Trim(VN_Util.toTrim);
				content = lineSplit[1].Trim(VN_Util.toTrim);
				yield break;
			}
			// Check if line is a command call
			else
			{
				IEnumerator thisCommand = CommandCall.TryCommand(line);
				bool commandResult = thisCommand.MoveNext();
				// If line is a command;
				if (commandResult)
				{
					// Finish rest of command processing
					while (commandResult)
					{
						yield return thisCommand.Current;
						commandResult = thisCommand.MoveNext();
					}
					speaker = "Narrator";
					content = "";
				}
				else
				{
					// Assume narrator is speaking
					speaker = "Narrator";
					content = line.Trim(VN_Util.toTrim);
					yield break;
				}
			}
		}
		#endregion

		// Methods to clear and/or reset objects on the Visual Novel
		#region VN Clearing

		// Clears all text, buttons, and names from the Visul Novel
		void ClearContent()
		{
			// Reset all internal references
			contentTextObj = null;
			currentLine = null;
			currentSpeaker = null;
			currentTags = null;

			// Destroys all the children of all Canvases
			foreach (Transform child in TextCanvas.transform)
			{
				Destroy(child.gameObject);
			}

			foreach (Transform child in ButtonCanvas.transform)
			{
				Destroy(child.gameObject);
			}

			foreach (Transform child in NameCanvas.transform)
			{
				Destroy(child.gameObject);
			}
		}
		#endregion

		#region VN Control

		public void ForceExitVN()
		{
			TextboxData data = textboxManager.data;
			StartCoroutine(textboxManager.HideTextbox(data));
			StartCoroutine(characterManager.ResetCharacters());
		}

		public void StartVN()
		{
			StartStory();
		}

		public void StartVN(TextboxData data)
		{
			StartCoroutine(Co_StartVN(data));
		}

		private IEnumerator Co_StartVN(TextboxData data)
		{
			activeState = ActiveState.active;
			yield return StartCoroutine(data.textboxTransition.Co_EnterScreen(this, this));
			StartStory();
		}

		public void ToggleMouseState()
		{
			if (mouseState == MouseState.other)
			{
				mouseState = MouseState.contentBox;
			}
			else
			{
				mouseState = MouseState.other;
			}

		}

		#endregion

		#region Other

		/**
		 * Sets the speed of the text.
		 * Pause speed will be 1/6 normal speed
		 * 
		 * @Param speed: the speed of normal text
		 */
		public void SetTextSpeed(float speed)
		{
			TextSpeeds["Normal"] = speed;
			TextSpeeds["Pause"] = speed / 6;
		}

		#endregion
	}
}