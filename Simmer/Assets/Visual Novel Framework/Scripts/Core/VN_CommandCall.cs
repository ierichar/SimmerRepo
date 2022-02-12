using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simmer.VN
{
	public class VN_CommandCall : MonoBehaviour
	{
		private VN_Manager _manager;

		// Inky custom command calling
		private readonly string CommandCallStringNormal = ">>>";
		private readonly char[] MultiCommandChar = { ';' };
		private readonly char[] CommandDelimeters = { ',', '(', ')' };
		private readonly char ImmediateMarker = '!';
		// Store/call funcitons in a dictionary https://stackoverflow.com/questions/4233536/c-sharp-store-functions-in-a-dictionary
		Dictionary<string, Delegate> AllCommands =
			new Dictionary<string, Delegate>();

		public void Construct(VN_Manager VN_Manager)
		{
			_manager = VN_Manager;

			List<ICommandCall> commandCalls = FindObjectsOfType<MonoBehaviour>()
				.OfType<ICommandCall>().ToList();
			// Populate AllCommands
			foreach (var command in commandCalls)
			{
				string commandName = VN_Util.RemoveSubstring(
					command.GetType().ToString(), "Cmd");
				commandName = VN_Util.RemoveSubstring(
					commandName, "Simmer.VN.");
				Func <List<string>, IEnumerator> newCommand = command.Command;

				AllCommands.Add(commandName, newCommand);
			}
		}

		public IEnumerator TryCommand(string line)
		{
			// Line needs to beging with exactly CommandCallStringNormal in the first 3 characters.
			if (line.Length > 3 && line.Substring(0, 3) == CommandCallStringNormal)
			{
				// Get a list of all the identified commands in the line by splitting by MultiCommandChar and removing any results that are empty
				List<string> commands = line.Substring(3).Split(MultiCommandChar, StringSplitOptions.RemoveEmptyEntries)
					.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
				if (commands.Count > 0)
				{
					yield return true;
				}

				// Try to run all commands
				foreach (string rawCommand in commands)
				{
					var command = rawCommand.Trim(VN_Util.toTrim);
					bool isImmediate = false;

					// Split function name and args
					string[] commandArray = command.Split(CommandDelimeters, StringSplitOptions.RemoveEmptyEntries);
					// Convert to list
					List<string> commandList = new List<string>();
					foreach (string s in commandArray)
					{
						commandList.Add(s.Trim(VN_Util.toTrim));
					}

					string function = commandList[0];
					// Check if first char in function is ImmediateMarker
					if (function[0] == ImmediateMarker)
					{
						function = function.Trim(ImmediateMarker);
						isImmediate = true;
					}
					// Assume rest of contents are args
					List<string> arguments = commandList.GetRange(1, commandList.Count - 1);

					if (AllCommands.ContainsKey(function))
					{
						// Get reference to the function from AllCommands
						Func<List<string>, IEnumerator> Co_Command =
							(Func<List<string>, IEnumerator>)AllCommands[function];

						if (VN_Util.VN_Debug)
						{
							string args = string.Join(" ", arguments);
							VN_Util.VNDebugPrint("Calling command: " + " \"" +
								function + "(" + args + ")\"", this);
						}

						// isImmediate false yields the coroutine thereby waiting for it to finish
						// while true doesn't wait
						if (isImmediate)
						{
							StartCoroutine(Co_Command(arguments));
						}
						else
						{
							yield return StartCoroutine(Co_Command(arguments));
						}
					}
					else
					{
						Debug.LogError("AllCommands doesn't contain key \"" + function + "\"");
					}
				}
			}
		}
	}
}