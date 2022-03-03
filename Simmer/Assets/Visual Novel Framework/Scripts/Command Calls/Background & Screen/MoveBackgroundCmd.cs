using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.VN
{
    public class MoveBackgroundCmd : MonoBehaviour, ICommandCall
    {
        public IEnumerator Command(List<string> args)
        {
            VN_Manager manager = VN_Util.manager;

            VN_ScreenManager screenManager = manager.screenManager;

            float newX = 0;
            float newY = 0;
            float duration = 1;

            if (args.Count == 2)
            {
                newX = float.Parse(args[0]);
                newY = float.Parse(args[1]);
            }
            else if (args.Count == 3)
            {
                newX = float.Parse(args[0]);
                newY = float.Parse(args[1]);
                duration = float.Parse(args[2]);
            }
            else
            {
                Debug.LogError(this + " args error");
            }


            yield return StartCoroutine(screenManager.MoveBackground(newX, newY, duration));
        }
    }
}