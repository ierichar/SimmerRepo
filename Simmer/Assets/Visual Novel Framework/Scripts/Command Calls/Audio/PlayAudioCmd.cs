using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.VN
{
    public class PlayAudioCmd : MonoBehaviour, ICommandCall
    {
        public IEnumerator Command(List<string> args)
        {
            VN_Manager manager = VN_Util.manager;

            VN_AudioManager audioManager = manager.audioManager;

            bool result = audioManager.PlayAudio(args[0]);

            if (!result)
            {
                Debug.LogError("Couldn't find audio clip \"" + args[0] + "\" in VN_AudioManager");
            }
            yield return null;
        }
    }
}