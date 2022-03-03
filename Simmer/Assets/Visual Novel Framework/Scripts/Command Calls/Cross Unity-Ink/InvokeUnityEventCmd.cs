using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Simmer.VN
{
    public class InvokeUnityEventCmd : MonoBehaviour, ICommandCall
    {
        IEnumerator ICommandCall.Command(List<string> args)
        {
            if(args.Count != 1)
            {
                Debug.LogError("Arg number error: " + this);
                yield break;
            }

            VN_SharedVariables sharedVariables = VN_Util.manager.sharedVariables;

            sharedVariables.InvokeEvent(args[0]);
        }
    }
}