using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.VN
{
    public class TextboxEnterCmd : MonoBehaviour, ICommandCall
    {
        public IEnumerator Command(List<string> args)
        {
            VN_Manager manager = VN_Util.manager;

            if (args.Count == 1)
            {
                TextboxData data = VN_Util.FindTextboxData(args[0]);
                yield return StartCoroutine(manager.textboxManager.ShowTextbox(data, null));
            }
            else if (args.Count == 2)
            {
                TextboxData data = VN_Util.FindTextboxData(args[0]);
                Sprite decor = VN_Util.FindTextboxCornerDecor(data, args[1]);
                yield return StartCoroutine(manager.textboxManager.ShowTextbox(data, decor));
            }
            else
            {
                Debug.LogError("Arg number error: " + this);
                yield break;
            }
        }
    }
}