using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.VN
{
    public class FadeBlackCmd : MonoBehaviour, ICommandCall
    {
        public IEnumerator Command(List<string> args)
        {
            VN_Manager manager = VN_Util.manager;
            VN_ScreenManager screenManager = manager.screenManager;

            float endFade = 0;
            float duration = 1;

            if (args.Count == 1)
            {
                endFade = float.Parse(args[0]);
            }
            else if (args.Count == 2)
            {
                endFade = float.Parse(args[0]);
                duration = float.Parse(args[1]);
            }
            else
            {
                Debug.LogError(this + " args error");
            }


            yield return StartCoroutine(screenManager.FadeBlack(endFade, duration));
        }
    }
}