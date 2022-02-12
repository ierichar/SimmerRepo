using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

namespace Simmer.VN
{
    public class SetUnityVarCmd : MonoBehaviour, ICommandCall
    {
        public IEnumerator Command(List<string> args)
        {
            if (args.Count != 2)
            {
                Debug.LogError("Arg number error: " + this);
                yield break;
            }

            Story Story = VN_Util.manager.Story;
            VN_SharedVariables sharedVariables = VN_Util.manager.sharedVariables;

            sharedVariables.SetVariable(args[0], args[1]);
            yield break;
        }
    }
}