using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Simmer.VN
{
    public class WaitUntilEventCmd : MonoBehaviour, ICommandCall
    {
        bool isWaitingForEvent = true;

        public IEnumerator Command(List<string> args)
        {

            if (args.Count != 1)
            {
                Debug.LogError("Arg number error: " + this);
                yield break;
            }

            isWaitingForEvent = true;

            VN_SharedVariables sharedVariables = VN_Util.manager.sharedVariables;

            UnityEvent targetEvent = sharedVariables.GetEvent(args[0]);
            targetEvent.AddListener(WaitUntil);

            yield return new WaitUntil(() => isWaitingForEvent == false);

            targetEvent.RemoveListener(WaitUntil);
        }

        private void WaitUntil()
        {
            isWaitingForEvent = false;
        }
    }
}